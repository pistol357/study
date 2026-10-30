using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 구글 시트에서 받은 CSV를 읽어 SkillData, CharacterData 에셋을 생성하거나 갱신한다.
/// </summary>
public static class DataImporter
{
    private const string SKILL_CSV_PATH = "Assets/Data/CSV/Skill.csv";
    private const string CHARACTER_CSV_PATH = "Assets/Data/CSV/Character.csv";
    private const string DATA_FOLDER = "Assets/Data";
    private const string SKILL_FOLDER_NAME = "Skills";
    private const string CHARACTER_FOLDER_NAME = "Characters";

    [MenuItem("Tools/Data/Import CSV")]
    private static void ImportAll()
    {
        // 캐릭터가 고유 마법을 스킬 Id로 찾기 때문에 스킬을 먼저 변환한다.
        Dictionary<int, SkillData> skills = ImportSkills();
        ImportCharacters(skills);

        AssetDatabase.SaveAssets();
        Debug.Log("CSV 변환 완료");
    }

    private static Dictionary<int, SkillData> ImportSkills()
    {
        string folder = EnsureFolder(SKILL_FOLDER_NAME);
        Dictionary<int, SkillData> skills = new Dictionary<int, SkillData>();

        foreach (List<string> row in ReadRows(SKILL_CSV_PATH))
        {
            int id = int.Parse(row[0]);
            SkillType type = (SkillType)Enum.Parse(typeof(SkillType), row[1]);
            Sprite icon = LoadIcon(row[3]);
            float cooldown = float.Parse(row[5], CultureInfo.InvariantCulture);

            SkillData skill = LoadOrCreate<SkillData>($"{folder}/Skill_{id}.asset");
            skill.SetData(id, type, row[2], icon, row[4], cooldown);
            EditorUtility.SetDirty(skill);

            skills[id] = skill;
        }

        return skills;
    }

    private static void ImportCharacters(Dictionary<int, SkillData> skills)
    {
        string folder = EnsureFolder(CHARACTER_FOLDER_NAME);

        foreach (List<string> row in ReadRows(CHARACTER_CSV_PATH))
        {
            int id = int.Parse(row[0]);
            int hp = int.Parse(row[2]);
            int mp = int.Parse(row[3]);
            float mpRegen = float.Parse(row[4], CultureInfo.InvariantCulture);
            float moveSpeed = float.Parse(row[5], CultureInfo.InvariantCulture);
            SkillData uniqueSkill1 = FindSkill(skills, row[7]);
            SkillData uniqueSkill2 = FindSkill(skills, row[8]);

            CharacterData character = LoadOrCreate<CharacterData>($"{folder}/Character_{id}.asset");
            character.SetData(id, row[1], hp, mp, mpRegen, moveSpeed, row[6], uniqueSkill1, uniqueSkill2);
            EditorUtility.SetDirty(character);
        }
    }

    // 기존 에셋을 지우지 않고 재사용해야, 이 에셋을 참조하던 곳의 연결이 끊기지 않는다.
    private static T LoadOrCreate<T>(string path) where T : ScriptableObject
    {
        T asset = AssetDatabase.LoadAssetAtPath<T>(path);

        if (asset == null)
        {
            asset = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(asset, path);
        }

        return asset;
    }

    private static string EnsureFolder(string folderName)
    {
        string path = $"{DATA_FOLDER}/{folderName}";

        if (!AssetDatabase.IsValidFolder(path))
        {
            AssetDatabase.CreateFolder(DATA_FOLDER, folderName);
        }

        return path;
    }

    private static Sprite LoadIcon(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return null;
        }

        Sprite icon = AssetDatabase.LoadAssetAtPath<Sprite>(path);

        if (icon == null)
        {
            Debug.LogWarning($"이미지를 찾을 수 없습니다 : {path}");
        }

        return icon;
    }

    private static SkillData FindSkill(Dictionary<int, SkillData> skills, string idText)
    {
        int id = int.Parse(idText);

        if (!skills.TryGetValue(id, out SkillData skill))
        {
            Debug.LogWarning($"스킬 Id {id}를 찾을 수 없습니다.");
        }

        return skill;
    }

    // 첫 줄(헤더)과 빈 줄은 건너뛴다.
    private static List<List<string>> ReadRows(string csvPath)
    {
        List<List<string>> rows = new List<List<string>>();
        string[] lines = File.ReadAllLines(csvPath);

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
            {
                continue;
            }

            rows.Add(SplitLine(lines[i]));
        }

        return rows;
    }

    // 구글 시트는 쉼표가 들어간 칸을 큰따옴표로 감싸서 내보내므로, 따옴표 안의 쉼표는 구분자로 보지 않는다.
    private static List<string> SplitLine(string line)
    {
        List<string> cells = new List<string>();
        StringBuilder cell = new StringBuilder();
        bool isInQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '"')
            {
                // 따옴표 안의 "" 는 따옴표 문자 하나를 뜻한다.
                if (isInQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    cell.Append('"');
                    i++;
                }
                else
                {
                    isInQuotes = !isInQuotes;
                }
            }
            else if (c == ',' && !isInQuotes)
            {
                cells.Add(cell.ToString().Trim());
                cell.Clear();
            }
            else
            {
                cell.Append(c);
            }
        }

        cells.Add(cell.ToString().Trim());

        return cells;
    }
}
