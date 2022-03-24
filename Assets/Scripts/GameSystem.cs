using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static List<PlayerComponent> AttackingPlayers = new List<PlayerComponent>();
    public static List<EnemyComponent> FrozenEnemies = new List<EnemyComponent>();


    void Update()
    {
        Debug.Log($"{AttackingPlayers.Count},{ FrozenEnemies.Count}");
        if (AttackingPlayers.Count == 2 && FrozenEnemies.Count != 0)
        {
            foreach(var enemy in FrozenEnemies)
            {
                enemy.canMove = false;
                
            }
        }
    }
}
