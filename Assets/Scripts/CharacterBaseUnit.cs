using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseUnit : MonoBehaviour
{
    protected float processedTurnInput;
    protected float processedLookInput;
    protected Vector3 processedMovementInput;
    protected float horizontalInput;
    protected float verticalInput;
    public float LookSpeed;
    public float TurnSpeed;
    public float Speed;
    public  Transform CameraContainer;
    protected Rigidbody rb;
    protected Animator anim;
    protected void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        anim = this.GetComponentInChildren<Animator>();
    }
    protected void MoveCamera()
    {
        CameraContainer.Rotate(new Vector3(processedLookInput, 0f, 0f) * LookSpeed * Time.deltaTime);
    }
    protected void Move()
    {
        processedMovementInput = transform.forward * verticalInput + transform.right * horizontalInput;
        rb.MovePosition(this.transform.position + processedMovementInput * Speed * Time.deltaTime);
        rb.MoveRotation(Quaternion.Euler(transform.eulerAngles + (Vector3.up * processedTurnInput) * TurnSpeed * Time.deltaTime)) ;
        anim.SetFloat("Horizontal", horizontalInput);
        anim.SetFloat("Vertical", verticalInput);
        if (horizontalInput != 0 || verticalInput != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }


}
