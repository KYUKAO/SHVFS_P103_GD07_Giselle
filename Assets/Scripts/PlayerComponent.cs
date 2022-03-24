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
        Cursor.lockState = CursorLockMode.Locked;

            processedTurnInput = Input.GetAxis("Mouse X"+playerID);
            processedLookInput = -Input.GetAxis("Mouse Y"+playerID);
            horizontalInput = Input.GetAxis("Horizontal"+playerID);
            verticalInput = Input.GetAxis("Vertical"+playerID);


        MoveCamera();
        Move();
    }
}
