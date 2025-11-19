using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public float health, maxHealth = 10;
    public bool isAlive = true;
    public static EnemyStat Instance;

    public List<string> EnemyWords;

    void Start()
    {
        Instance = this;
        health = maxHealth;
    }

    void Update()
    {
        if(health <= 0)
        {
            // Do something ... while enemy defeat
            isAlive = false;
        }
    }

    public float GetForFillableHealth()
    {
        return health / maxHealth;
    }

}
