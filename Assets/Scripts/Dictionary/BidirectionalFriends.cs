using System.Collections.Generic;
using UnityEngine;

namespace Dictionary
{
    public class BidirectionalFriends : MonoBehaviour
    {
        [SerializeField] private List<FriendData> friendsList;
        private readonly Dictionary<string, List<string>> _friends = new Dictionary<string, List<string>>();

        private void Start()
        {
            _friends.Add("Alice", new List<string>{"Glu"});
            Connect("Alice", "Bob");

            ToList(_friends);
        }

        private void Connect(string personA, string personB)
        {
            if (personA == personB) return;
        
            AddOneWay(personA, personB);
            AddOneWay(personB, personA);
        }
        private void AddOneWay(string from, string to)
        {
            if (_friends.TryGetValue(from, out var list))
            {
                if (!list.Contains(to)) list.Add(to);
            }
            else _friends.Add(from, new List<string> { to });
        }

        private void Disconnect(string personA, string personB)
        {
            RemoveOneWay(personA, personB);
            RemoveOneWay(personB, personA);
        }
        private void RemoveOneWay(string from, string to)
        {
            if (_friends.TryGetValue(from, out var list))
            {
                if (!list.Remove(to)) Debug.LogWarning($"{to} not found in {from}");
            }
            else Debug.LogWarning($"{from} not found");
        }

        private void ToList(Dictionary<string, List<string>> friends)
        {
            friendsList.Clear();
            foreach (var pair in friends)
            {
                FriendData friend = new FriendData()
                {
                    person = pair.Key,
                    friends = new List<string>(pair.Value)
                };
                friendsList.Add(friend);
            }
        }

        private void ToDictionary(List<FriendData> friends)
        {
            _friends.Clear();
            foreach (var pair in friends)
            {
                if (string.IsNullOrEmpty(pair.person))
                {
                    Debug.LogWarning("Person is null or empty");
                    continue;
                }

                if (!_friends.TryAdd(pair.person, new List<string>(pair.friends ?? new List<string>())))
                {
                    Debug.LogWarning($"{pair.person} is duplicate");
                }
            }
        }
    }
    
    [System.Serializable]
    public class FriendData
    {
        public string person;
        public List<string> friends;
    }
}
