using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterBaseUnit : MonoBehaviour
{
    #region Variables
    protected float processedTurnInput;
    protected float processedLookInput;
    protected Vector3 processedMovementInput;
    protected float horizontalInput;
    protected float verticalInput;
    public float LookSpeed;
    public float TurnSpeed;
    public float Speed;
    public Transform CameraContainer;
    protected Rigidbody rb;
    protected Animator anim;
    public float CurrentHealth;
    protected float previousHealth;
    public float MaxHealth;
    public float DeliverHealth;
    float CameraRotateMaxAngle=100f;
    float CameraRotateMinAngle;
    #endregion

    protected void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        anim = this.GetComponentInChildren<Animator>();
        CurrentHealth = MaxHealth;
        previousHealth = CurrentHealth;
    }

    protected void MoveCamera()
    {
            CameraContainer.Rotate(new Vector3(processedLookInput, 0f, 0f) * LookSpeed * Time.deltaTime);
    }

    protected void Move()
    {
        processedMovementInput = (transform.forward * verticalInput + transform.right * horizontalInput).normalized;
        rb.MovePosition(this.transform.position + processedMovementInput * Speed * Time.deltaTime);
    }

    protected void RotateBody()
    {
        rb.MoveRotation(Quaternion.Euler(transform.eulerAngles + (Vector3.up * processedTurnInput) * TurnSpeed * Time.deltaTime));
    }

    protected void AnimationControl()
    {
        anim.SetFloat("Horizontal", horizontalInput);
        anim.SetFloat("Vertical", verticalInput);
        anim.SetBool("isMoving", (horizontalInput != 0 || verticalInput != 0));
    }

    protected void HealthControl()
    {
        if (previousHealth > CurrentHealth)
        {
            anim.SetBool("isDamaged", true);
            previousHealth = CurrentHealth;
        }
        else
        {
            anim.SetBool("isDamaged", false);
        }
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
        }
    }
}
