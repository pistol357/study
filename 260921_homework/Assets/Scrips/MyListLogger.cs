using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyListLogger : MonoBehaviour
{
    private void Start()
    {
        RunOperations();
    }

    private void RunOperations()
    {
        MyList<string> monsters = new MyList<string>();

        // 슬라임을 Add하고 LogStep("슬라임 넣기", monsters)를 부릅니다.  
        monsters.Add("슬라임");
        LogStep("슬라임 넣기", monsters);
        // 고블린을 Add하고 LogStep("고블린 넣기", monsters)를 부릅니다.  
        // 오크를 Add하고 LogStep("오크 넣기", monsters)를 부릅니다.  
        // 트롤을 Add하고 LogStep("트롤 넣기", monsters)를 부릅니다.  
        // 드래곤을 Add하고 LogStep("드래곤 넣기", monsters)를 부릅니다.  
        // 1번 자리에 늑대를 Insert하고 LogStep("1번 자리에 늑대 넣기", monsters)를 부릅니다.  
        // 오크의 번호를 IndexOf로 찾아 LogIndex("오크 찾기", 찾은 번호)를 부릅니다.  
        // 리치가 들어 있는지 Contains로 확인해 LogBool("리치가 들어 있는가", 확인한 값)을 부릅니다.  
        // 고블린을 Remove하고 LogBool("고블린 지우기", 돌려받은 값)을 부릅니다.  
        // 이어서 LogStep("고블린 지운 뒤", monsters)를 부릅니다.  
        // 0번 자리를 RemoveAt하고 LogStep("0번 자리 지우기", monsters)를 부릅니다.  
        // Clear로 비우고 LogStep("비우기", monsters)를 부릅니다.  
    }

    private void LogStep(string label, MyList<string> monsters)
    {
        Debug.Log($"MyListLogger: {label} / 개수 {monsters.Count} / 칸 수 {monsters.Capacity} / 내용 {monsters.ToText()}");
    }

    private void LogIndex(string label, int index)
    {
        Debug.Log($"MyListLogger: {label} / 번호 {index}");
    }

    private void LogBool(string label, bool value)
    {
        Debug.Log($"MyListLogger: {label} / 결과 {value}");
    }
}
