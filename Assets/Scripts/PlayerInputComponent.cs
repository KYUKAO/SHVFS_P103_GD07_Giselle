using UnityEngine;

public class PlayerInputComponent : MonoBehaviour
{
    Rigidbody rigidBody;
    public float Speed;
    public Transform CameraContainer;
    bool isGrounded = false;
    bool canJump = false;
    public float JumpHeight;
    public float TurnSpeed;
    public float LookSpeed;
    float mouseX;
    float mouseY;
    Vector3 processedMovementInput;
    float processedTurnInput;
    float processedLookInput;
    public Transform Shooter;
    Animator anim;
     GameObject bullet;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        bullet = Resources.Load<GameObject>("Sphere");
    }

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        processedTurnInput = mouseX;
        mouseY = Input.GetAxis("Mouse Y");
        processedLookInput = -mouseY;


        Cursor.lockState = CursorLockMode.Locked;
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        processedMovementInput = transform.forward * verticalInput + transform.right * horizontalInput;
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
        //Move things in Update,Use time.deta time so that things execute with frame rate;g
        if ((Input.GetKeyDown(KeyCode.Space) && (isGrounded == true)))
        {
            isGrounded = false;
            canJump = true;
        }
        CameraContainer.Rotate(new Vector3(processedLookInput, 0f, 0f) * LookSpeed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        rigidBody.MovePosition(this.transform.position + processedMovementInput * Speed * Time.deltaTime);
        //rigidBody.AddForce(processedInput * speed * Time.fixedDeltaTime);
        rigidBody.MoveRotation(Quaternion.Euler(transform.eulerAngles + (Vector3.up * processedTurnInput) * TurnSpeed * Time.deltaTime));
        if (canJump)
        {
            anim.SetTrigger("jumpTrigger");
            rigidBody.AddForce(0f, JumpHeight, 0f);
            canJump = false;
        }
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<TerrainCollider>() != null)
        {
            isGrounded = true;
        }
    }
    private void Shoot()
    {
        var newBullet = Instantiate(bullet, Shooter.transform.position,Shooter.transform.rotation);
    }
}
