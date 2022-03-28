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
    void Start()
    {

    }
    [SerializeField]
    PlayerID playerID;

    void Update()
    {
        if (Input.GetButtonDown("Fire"+playerID))
        {
            Shoot();
        }
       // Cursor.lockState = CursorLockMode.Locked;

        RayCast();

            processedTurnInput = Input.GetAxis("Mouse X"+playerID);
            processedLookInput = -Input.GetAxis("Mouse Y"+playerID);
            horizontalInput = Input.GetAxis("Horizontal"+playerID);
            verticalInput = Input.GetAxis("Vertical"+playerID);
        MoveCamera();
        AnimationControl();

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Die()
    {

    }
}
