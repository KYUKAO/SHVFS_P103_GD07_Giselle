using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : Singleton<GameSystem>
{
    public  List<PlayerComponent> AttackingPlayers = new List<PlayerComponent>();
    public  List<EnemyComponent> TargetEnemies = new List<EnemyComponent>();
    public float Power;
    public  bool isGameOver = false;
    public GameObject GameOver;
    public GameObject WinInterface;
    public Text EnemyNumText;
    public EnemyComponent[] enemies;
    private void Start()
    {
        Time.timeScale = 1;
        enemies = FindObjectsOfType<EnemyComponent>();
        if (GameOver)
        {
            GameOver.SetActive(false);
        }
        if (WinInterface)
        {
            WinInterface.SetActive(false);
        }
    }

    void Update()
    {
        //Debug.Log($"{AttackingPlayers.Count},{ TargetEnemies.Count}");
        if (AttackingPlayers.Count == 2 && TargetEnemies.Count != 0)
        {
            foreach(var enemy in TargetEnemies)
            {
                enemy.CurrentHealth -= Power * Time.deltaTime;
            }
        }
        if (GameOver)
        {
            if (isGameOver)
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                GameOver.SetActive(true);
            }
        }
        if (WinInterface)
        {
            if (enemies.Length == 0)
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                WinInterface.SetActive(true);
            }
        }
        if (EnemyNumText)
        {
            EnemyNumText.text = "Enemy :"+enemies.Length;
        }
    }
}
