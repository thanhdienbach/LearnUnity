using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreManager : MonoBehaviour
{
    private Dictionary<string, int> score = new Dictionary<string, int>();
    void Start()
    {
        AddNewPlayer("Phuoc", 10);
        AddScore("Phuoc", 20);
        Debug.Log(GetScore("Phuoc"));
    }

    private void AddNewPlayer(string playerName, int value)
    {
        if (!score.TryAdd(playerName, value)) Debug.LogWarning("Player " + playerName + " already existed!");
    }

    public void AddScore(string playerName, int value)
    {
        if (score.TryGetValue(playerName, out int currentScore))
        {
            score[playerName] = currentScore + value;
        }
        else
        {
            Debug.LogWarning($"Player {playerName} not found!");
        }
    }

    public int GetScore(string playerName)
    {
        return score.TryGetValue(playerName, out int currentScore) ? currentScore : 0;
    }
}
