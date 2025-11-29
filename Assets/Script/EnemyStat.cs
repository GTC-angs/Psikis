using System.Collections.Generic;
using NUnit.Framework;
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
        if (health <= 0 && isAlive)
        {
            // Do something ... while enemy defeat
            isAlive = false;
            EnemyMovement.Instance.isCanMove = false;
            if (Scene05Manager.Instance != null)
            {
                Scene05Manager.Instance.WinGame();
            }
        }
    }

    public float GetForFillableHealth()
    {
        return health / maxHealth;
    }

}
