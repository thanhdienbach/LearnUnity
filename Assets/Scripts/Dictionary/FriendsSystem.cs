using System.Collections.Generic;
using UnityEngine;

public class FriendsSystem : MonoBehaviour
{
    private Dictionary<string, List<string>> friends = new Dictionary<string, List<string>>();

    
    private void Start()
    {
        AddFriend("Alice", "Bob");
        AddFriend("Bob", "Charlie");
        
        RemoveFriend("Alice", "Bob");
        
        GetFriends("Alice");

        PrintAllFriends();
    }

    
    public void AddFriend(string personA, string personB)
    {
        if (!friends.TryGetValue(personA, out var list))
        {
            list = new List<string>();
            friends.Add(personA, list);
        }

        if (!list.Contains(personB))
        {
            list.Add(personB);
        }
    }

    public void RemoveFriend(string personA, string personB)
    {
        if (friends.TryGetValue(personA, out List<string> friendList))
        {
            if (!friendList.Remove(personB))
            {
                Debug.LogWarning($"Person {personB} is not in {personA}'s list");
            }
        }
        else
        {
            Debug.LogWarning($"{personA} is not found");
        }
    }

    public List<string> GetFriends(string personA)
    {
        if (!friends.TryGetValue(personA, out var list))
        {
            Debug.LogError($"Friend {personA} not found");
            return  new List<string>();
        }
        return new List<string>(list);
    }

    public void PrintAllFriends()
    {
        foreach (var friend in friends)
        {
            Debug.Log($"Bạn {friend.Key} có bạn là:");
            foreach (var person in friend.Value)
            {
                Debug.Log("-" + person);
            }
        }
    }
}
