/// <summary>
/// 스킬의 종류.
/// </summary>
public enum SkillType
{
    Attack,
    Support,
    Disrupt,
    Move
}

/// <summary>
/// SkillType을 화면에 표시할 이름으로 바꾼다.
/// </summary>
public static class SkillTypeExtensions
{
    /// <summary>
    /// 화면 표시용 한글 이름을 돌려준다.
    /// </summary>
    public static string ToDisplayName(this SkillType type)
    {
        // enum 이름은 CSV와 맞춰야 해서 바꿀 수 없으므로, 표시용 이름을 따로 둔다.
        switch (type)
        {
            case SkillType.Attack:
                return "공격";
            case SkillType.Support:
                return "지원";
            case SkillType.Disrupt:
                return "방해";
            case SkillType.Move:
                return "이동";
            default:
                // 값을 추가하고 이 표를 깜빡해도 빈칸 대신 enum 이름이 보이게 한다.
                return type.ToString();
        }
    }
}
