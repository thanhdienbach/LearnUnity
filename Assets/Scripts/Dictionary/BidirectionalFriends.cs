using System.Collections.Generic;
using System.Linq;
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
            Disconnect("Alice", "Glu");

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

                var friendList = (pair.friends ?? new List<string>()).Distinct().ToList();
                if (!_friends.TryAdd(pair.person, friendList))
                {
                    Debug.LogWarning($"{pair.person} is duplicate");
                }
            }
        }

        private bool IsValid()
        {
            var countDirty = 0;
            foreach (var pair in _friends)
            {
                string person = pair.Key;
                var friends = pair.Value;
                
                // 1. Person không null, person không rỗng
                if (string.IsNullOrEmpty(person))
                {
                    Debug.LogWarning($"Invalid key detected (null or empty)");
                    countDirty++;
                }
                
                // 2. Friend không null, friend không rỗng
                if (friends == null)
                {
                    Debug.LogWarning($"{person} has null friend list");
                    countDirty++;
                    continue;
                }
                for (int i = 0; i < friends.Count; i++)
                {
                    if (string.IsNullOrEmpty(friends[i]))
                    {
                        Debug.LogWarning($"{person} has invalid friend at index {i}"); 
                        countDirty++;
                    }
                }
                
                // 3. Friend không duplicate
                for (int i = 0; i < friends.Count; i++)
                {
                    for (int j = i + 1; j < friends.Count; j++)
                    {
                        if (friends[i] == friends[j])
                        {
                            Debug.LogWarning($"{person} has duplicate friend {friends[i]}");
                            countDirty++;
                        }
                    }
                }
                
                // 4. Không tự là bạn của bản thân
                if (friends.Contains(person))
                {
                    Debug.LogWarning($"{person} is friend self");
                    countDirty++;
                }
                
                // 5. Phải là bạn của nhau
                foreach (var friend in friends)
                {
                    if (string.IsNullOrEmpty(friend)) continue;
                    if (!_friends.TryGetValue(friend, out var reverseList))
                    {
                        Debug.LogWarning($"{friend} missing key");
                        countDirty++;
                    }
                    else if (!reverseList.Contains(person))
                    {
                        Debug.LogWarning($"{person} -> {friend} not symmetric");
                        countDirty++;
                    }
                }
            }
            if (countDirty > 0) Debug.LogWarning($"Have {countDirty} dirty data");
            return countDirty == 0;
        }

        private void FixData()
        {
            FixInvalidPair(_friends);
            FixSymmetry(_friends);
        }
        // 1. Xóa key và value, xử lý value không hợp lệ
        private void FixInvalidPair(Dictionary<string, List<string>> friends)
        {
            foreach (var key in friends.Keys.ToList())
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    friends.Remove(key);
                    continue;
                }
                var list = friends[key] ??= new List<string>();
                list.RemoveAll(string.IsNullOrWhiteSpace);
                var set = new HashSet<string>();
                list.RemoveAll(x => x == key); // Remove chính mình
                list.RemoveAll(x => !set.Add(x)); // Remove duplicate
            }
        }
        // 2. Xử lý symmetry
        private void FixSymmetry(Dictionary<string, List<string>> friends)
        {
            foreach (var pair in friends.ToList())
            {
                var person = pair.Key;
                var list = pair.Value;
                foreach (var friend in list.ToList())
                {
                    if (!friends.TryGetValue(friend, out var reverseList))
                    {
                        friends[friend] = new List<string>() { person };
                    }
                    else if (!reverseList.Contains(person))
                    {
                        reverseList.Add(person);
                    }
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
