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
    public float RayLength;
    public Vector3 Offset;


    public float CurrentHealth;
    public float MaxHealth;

    public GameObject BulletPrefab;
    GameObject enemy = null;
    public Transform GunContainer;
    EnemyComponent enemyComponent;

    #endregion

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
        processedMovementInput = (transform.forward * verticalInput + transform.right * horizontalInput).normalized;
        rb.MovePosition(this.transform.position + processedMovementInput * Speed * Time.deltaTime);
        rb.MoveRotation(Quaternion.Euler(transform.eulerAngles + (Vector3.up * processedTurnInput) * TurnSpeed * Time.deltaTime));
    }
    protected void AnimationControl()
    {
        anim.SetFloat("Horizontal", horizontalInput);
        anim.SetFloat("Vertical", verticalInput);
        //anim.SetBool("IsMoving", horizontalInput != 0 || verticalInput != 0)
        if (horizontalInput != 0 || verticalInput != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }
    public void RayCast()
    {
        Debug.DrawRay(this.transform.position + Offset, CameraContainer.transform.forward * RayLength, Color.red);
        if (Physics.Raycast(this.transform.position + Offset, CameraContainer.transform.forward, out RaycastHit raycast, RayLength))
        {
            enemy = raycast.collider.gameObject;
            if (enemy.GetComponent<EnemyComponent>()!= null)
            {
                enemyComponent = enemy.GetComponent<EnemyComponent>();
                AddOnce<EnemyComponent>(GameSystem.FrozenEnemies, enemyComponent);
                AddOnce<PlayerComponent>(GameSystem.AttackingPlayers, this.GetComponent<PlayerComponent>());
            }
            else
            {
                RemoveOnce<PlayerComponent>(GameSystem.AttackingPlayers, this.GetComponent<PlayerComponent>());
                RemoveOnce<EnemyComponent>(GameSystem.FrozenEnemies, enemyComponent);
            }
        }
    }

    protected void Shoot()
    {
        Instantiate(BulletPrefab, GunContainer.position,GunContainer.transform.rotation);
    }
    void AddOnce<T>(List<T> team, T member)
    {
       if(!team.Contains(member))
        {
            team.Add(member);
        }
    }
    void RemoveOnce<T>(List<T>team,T member)
    {
        if (team.Contains(member))
        {
            team.Remove(member);
        }
    }
}
