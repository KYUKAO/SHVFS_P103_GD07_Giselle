using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComponent : CharacterBaseUnit
{
    public bool canMove;
    float timer;
    public float intervalTime;
    void Start()
    {
        timer = intervalTime;
    }

    void Update()
    {

        timer += Time.deltaTime;
        if (!GameSystem.FrozenEnemies.Contains(this.GetComponent<EnemyComponent>()))
        {
            canMove = true;
        }
        if (canMove)
        {
            if (timer >= intervalTime)
            {
                processedLookInput = Random.Range(-1, 2);
                horizontalInput = Random.Range(-1, 2);
                verticalInput = Random.Range(-1, 2);
                timer = 0;
            }
            Move();
        }
        else
        {
            processedLookInput = 0;
            horizontalInput = 0;
            verticalInput = 0;
        }
        AnimationControl();
    }
}
