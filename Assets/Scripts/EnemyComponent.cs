using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComponent : CharacterBaseUnit
{
    float timer;
    public float intervalTime;
    GameObject Player1;
    GameObject Player2;
    public float detectRadius;
    public float bindRadius;
    GameObject bindedPlayer=null;
    public float Damage;
    bool isActivated = false;

    void Start()
    {
        timer = intervalTime;
        var players = FindObjectsOfType<PlayerComponent>();
        foreach(var player in players)
        {
            if (player.playerID == PlayerComponent.PlayerID._P1)
            {
                Player1 = player.gameObject;
            }
            else if (player.playerID == PlayerComponent.PlayerID._P2)
            {
                Player2 = player.gameObject;
            }
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        void Die()
        {
            Destroy(this.gameObject);
        }
        if (timer >= intervalTime&&(!isActivated))
        {
            processedLookInput = Random.Range(-1, 2);
            horizontalInput = Random.Range(-1, 2);
            verticalInput = Random.Range(-1, 2);
            timer = 0;
        }
        AnimationControl();
        HealthControl();
        if (!isActivated)
        {
            if (Player1)
            {
                DetectPlayer(Player1);
            }
            if (Player2)
            {
                DetectPlayer(Player2);
            }
        }
        else if(bindedPlayer.GetComponent<PlayerComponent>()!=null)
        {
            bindedPlayer.GetComponent<PlayerComponent>().CurrentHealth -= Damage * Time.deltaTime;
        }
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        Move();
        RotateBody();
    }

    void DetectPlayer(GameObject player)
    {
        if ((Vector3.Distance(player.transform.position, this.transform.position) < detectRadius)
            &&(Player1.GetComponent<PlayerComponent>().CanMove==true)
            && (Player2.GetComponent<PlayerComponent>().CanMove == true))
        {
            horizontalInput = (player.transform.position - this.transform.position).normalized.x;
            verticalInput = (player.transform.position - this.transform.position).normalized.z;
            if(Vector3.Distance(player.transform.position, this.transform.position) < bindRadius)
            {
                horizontalInput = 0;
                verticalInput = 0;
                isActivated = true;
                if (player.GetComponent<PlayerComponent>()!=null)
                {
                    bindedPlayer = player;
                    bindedPlayer.GetComponent<PlayerComponent>().CanMove = false;
                }
            }
        }
    }

    private void OnDestroy()
    {
        GameSystem.Instantce.enemies = FindObjectsOfType<EnemyComponent>();
        if (bindedPlayer)
        {
            bindedPlayer.GetComponent<PlayerComponent>().CanMove = true;
        }
    }
}
