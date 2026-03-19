using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    private readonly Dictionary<int, Turret> _turrets = new Dictionary<int, Turret>();

    private void Start()
    {
        AddTurret(1, new Turret("Turret1", 10));
        AddTurret(2, new Turret("Turret2",20));
        AddTurret(1, new Turret("Turret1", 30));

        TryGetTurret(3, out Turret currentTurret); 

        RemoveTurret(999);

        UpdateAllTurrets();
    }
    

    public void AddTurret(int id, Turret turret)
    {
        if(!_turrets.TryAdd(id, turret)) Debug.LogWarning("[TurretManager] Turret " + id + " already exists");
    }

    public void RemoveTurret(int id)
    {
        if (!_turrets.Remove(id)) Debug.LogWarning("[TurretManager] Turret " + id + " not found");
    }

    public void UpdateAllTurrets()
    {
        foreach (var turret in _turrets.Values)
        {
            turret.TakeDamage();
        }
    }

    public bool TryGetTurret(int id, out Turret turret)
    {
        return _turrets.TryGetValue(id, out turret);
    }
}

public class Turret
{
    public string Name;
    public int Damage;

    public Turret(string name, int damage)
    {
        Name = name;
        Damage = damage;
    }
    
    public void TakeDamage()
    {
        // Đâu là test đừng chấm điểm chổ này
        // Logic take dame là của đạn nhưng vì là test nên chổ này sẽ là logic take dame
        Debug.Log($"{Name} | {Damage}");
    }
}
