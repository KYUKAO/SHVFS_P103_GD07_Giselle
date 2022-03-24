using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    public Transform CameraContainer;
    protected Rigidbody rb;
    protected Animator anim;
    public float RayLength;
    public Vector3 Offset;
    protected List<PlayerComponent> attackingPlayers;
    GameObject enemy = null;
    public float CurrentHealth;
    public float MaxHealth;
    public GameObject BulletPrefab;
    public Transform GunContainer;

    protected void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        anim = this.GetComponentInChildren<Animator>();
        attackingPlayers = GameSystem.AttackingPlayers;
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
            if (enemy.GetComponent<EnemyComponent>() != null)
            {
                if (attackingPlayers.Contains(this.GetComponent<PlayerComponent>())) return;
                attackingPlayers.Add(this.GetComponent<PlayerComponent>());
                GameSystem.FrozenEnemies.Add(enemy.GetComponent<EnemyComponent>());
            }
        }
        else
        {
            if (attackingPlayers.Contains(this.GetComponent<PlayerComponent>()))
            {
                attackingPlayers.Remove(this.GetComponent<PlayerComponent>());
            }
            if (enemy==null||enemy.GetComponent<EnemyComponent>() == null) return;
            if (GameSystem.FrozenEnemies.Contains(enemy.GetComponent<EnemyComponent>()))
            {
                GameSystem.FrozenEnemies.Remove(enemy.GetComponent<EnemyComponent>());
            }
        }
    }
    protected void Shoot()
    {
        Instantiate(BulletPrefab, GunContainer.position,GunContainer.transform.rotation);
    }
}
