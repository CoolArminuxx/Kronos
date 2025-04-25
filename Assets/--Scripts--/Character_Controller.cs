using UnityEngine;
using System.Collections;

public class Character_Controller : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float JumpHeight;

    [Header("Mouse Customization")]
    [SerializeField] private float Sensitivity;

    private LayerMask ignoreLayer;
    private Transform cameraRot;
    private Transform modelRot;
    private Rigidbody rb;
    private float hInput;
    private float vInput;
    private float currentSpeed;
    private bool canJump;
    private float rotationX;

    private void Start()
    {
        ignoreLayer = LayerMask.NameToLayer("player");
        currentSpeed = MaxSpeed;
        cameraRot = GetComponentInChildren<Camera>().gameObject.transform;
        modelRot = GetComponentInChildren<CapsuleCollider>().gameObject.transform;
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
    }

    private void Update()
    {
        PlayerRotation();
        PlayerSprint();
        PlayerJump();
        PlayerMovement();
    }

    private void PlayerRotation()
    {
        /*
        //Camera rotation | Follows Mouse position
        cameraRot.eulerAngles = new Vector3
            (Mathf.Clamp(-Input.mousePosition.y * Sensitivity, -75, 75), Input.mousePosition.x * Sensitivity, 0);
        */
        rotationX += -Input.GetAxis("Mouse Y") * Sensitivity;
        rotationX = Mathf.Clamp(rotationX, -75, 75);
        cameraRot.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * Sensitivity, 0);
    }

    private void PlayerMovement()
    {
        //Keyboard Input Detections
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        //Set movement direction
        modelRot.eulerAngles = new Vector3(0, cameraRot.eulerAngles.y, 0);
        Vector3 playerDirection = modelRot.forward * vInput + modelRot.right * hInput;

        //Move player forward
        rb.AddForce(playerDirection.normalized * (currentSpeed * 2), ForceMode.Force);

        //Limit speed
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, currentSpeed);
    }

    private void PlayerSprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentSpeed *= 1.5f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = MaxSpeed;
        }
    }

    private void PlayerJump()
    {
        //Ground Detect
        Debug.DrawRay(transform.position, -transform.up * 1.2f);
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hitData;

        if (Physics.Raycast(ray, out hitData, 1.2f, ~ignoreLayer))
        {
            if (hitData.collider.CompareTag("ground"))
            {
                canJump = true;
            }
        }
        else
        {
            canJump = false;
        }

        //Check & Jump
        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            rb.AddForce(Vector2.up * JumpHeight, ForceMode.Impulse);
        }
    }
}
