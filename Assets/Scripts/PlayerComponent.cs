using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : CharacterBaseUnit
{
    public enum PlayerID
    {
        _P1,
        _P2,
    }
    public float RayLength;
    public Transform RayCastPoint;
    GameObject targetObj = null;
    EnemyComponent enemyComponent;
    [HideInInspector]
    public bool CanMove = true;
    public PlayerID playerID;

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        processedTurnInput = Input.GetAxis("Mouse X" + playerID);
        processedLookInput = -Input.GetAxis("Mouse Y" + playerID);
        horizontalInput = Input.GetAxis("Horizontal" + playerID);
        verticalInput = Input.GetAxis("Vertical" + playerID);
        RayCast();
        MoveCamera();
        HealthControl();
        if (CanMove)
        {
            AnimationControl();
        }
        if (CurrentHealth == 0)
        {
            GameSystem.Instantce.isGameOver = true;
        }
    }

    private void FixedUpdate()
    {
        RotateBody();
        if (CanMove)
        {
            Move();
        }
    }

    public void RayCast()
    {
        Debug.DrawRay(RayCastPoint.position, CameraContainer.transform.forward * RayLength, Color.red);
        if (Physics.Raycast(RayCastPoint.position, CameraContainer.transform.forward, out RaycastHit raycast, RayLength))
        {
            targetObj = raycast.collider.gameObject;
            if (targetObj.GetComponent<EnemyComponent>() != null)
            {
                enemyComponent = targetObj.GetComponent<EnemyComponent>();
                AddOnce<EnemyComponent>(GameSystem.Instantce.TargetEnemies, enemyComponent);
                AddOnce<PlayerComponent>(GameSystem.Instantce.AttackingPlayers, this.GetComponent<PlayerComponent>());
            }
            else
            {
                RemoveOnce<PlayerComponent>(GameSystem.Instantce.AttackingPlayers, this.GetComponent<PlayerComponent>());
                RemoveOnce<EnemyComponent>(GameSystem.Instantce.TargetEnemies, enemyComponent);
            }
            if (targetObj.GetComponent<PlayerComponent>() != null)
            {
                if (targetObj.GetComponent<PlayerComponent>().CanMove == true && Input.GetButton("Fire" + playerID)
                    &&targetObj.GetComponent<PlayerComponent>().CurrentHealth< targetObj.GetComponent<PlayerComponent>().MaxHealth)
                {
                    this.CurrentHealth -= DeliverHealth * Time.deltaTime;
                    targetObj.GetComponent<PlayerComponent>().CurrentHealth += DeliverHealth * Time.deltaTime;
                }
            }
        }
    }

    void AddOnce<T>(List<T> team, T member)
    {
        if (!team.Contains(member))
        {
            team.Add(member);
        }
    }

    void RemoveOnce<T>(List<T> team, T member)
    {
        if (team.Contains(member))
        {
            team.Remove(member);
        }
    }
}
