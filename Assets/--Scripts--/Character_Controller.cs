using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private float MaxSpeed;

    [Header("Mouse Customization")]
    [SerializeField] private float Sensitivity;

    private Transform cameraRot;
    private Transform modelRot;
    private Rigidbody rb;
    private float hInput;
    private float vInput;
    private float currentSpeed;

    private void Start()
    {
        currentSpeed = MaxSpeed;
        cameraRot = GetComponentInChildren<Camera>().gameObject.transform;
        modelRot = GetComponentInChildren<CapsuleCollider>().gameObject.transform;
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        //Keyboard Input Detections
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        //Camera rotation | Follows Mouse position
        cameraRot.eulerAngles = new Vector3
            (Mathf.Clamp(-Input.mousePosition.y * Sensitivity, -75, 75), Input.mousePosition.x * Sensitivity, 0);

        //Movement | Speed limit
        modelRot.eulerAngles = new Vector3(0, cameraRot.eulerAngles.y, 0);
        Vector3 playerDirection = modelRot.forward * vInput + modelRot.right * hInput;
        rb.AddForce(playerDirection.normalized * 10, ForceMode.Force);
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, currentSpeed);
    }
}
