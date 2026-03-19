using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnDictionary : MonoBehaviour
{
    public Dictionary<string, int> score = new Dictionary<string, int>();
    void Start()
    {
        score.Add("Player", 100); // Thêm phần tử mới với key = "Player", value = 100
        score["Player"] = 200; // Gán value mới cho key "Player" = 200
        
        score["Enemy"] = 50; // Thêm phần tử mới với key = "Enemy", value = 50
        
        Debug.Log(score["Player"]);
        
        score.Remove("Enemy"); // Xóa phần tử "Enemy khỏi dictionary score
        
        Debug.Log(score["Enemy"]); // Dòng này gây lỗi vì key "Enemy ã bị xóa trớc đó" => Dictionary không an toàn khi lấy dữ liệu bằng key
    }
    
}
