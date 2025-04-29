using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    public Inventory inventory;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float mouseSensitivity = 2f;
    private bool opened = false;
    private Rigidbody rb;
    private Vector3 movement;
    private bool isGrounded;
    GameObject doors;
    [SerializeField] private Camera cam;
    private Transform targetdoor;
    private string distance;
    private float distanceondoor;
    public bool lockmove = false;
    public Transform playerCamera; // Assign your camera here in the Inspector
    private float xRotation = 0f;
   

    void Start()
    {
        cam.enabled = false;
        doors = GameObject.FindWithTag("Door");
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (doors != null)
        {
            targetdoor = doors.transform;
        }
        else
        {
            Debug.LogWarning("No object found with tag: " + doors.tag);
        }
    }

    void Update()
    {
        Debug.Log(inventory.items.Count + "sizeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee");
        for (int i = 0; i < inventory.items.Count; i++)
        {
            Debug.Log(inventory.items[i]);
        }
            if (targetdoor != null)
            {
                distanceondoor = Vector3.Distance(transform.position, targetdoor.position);
                distance = "Distance: " + distanceondoor.ToString("F2") + "m";
                Debug.Log(distance);
            }

            // Get input
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveZ = Input.GetAxisRaw("Vertical");

            // Movement relative to player rotation
            Vector3 forwardMovement = transform.forward * moveZ;
            Vector3 rightMovement = transform.right * moveX;
            movement = (forwardMovement + rightMovement).normalized;

            // Jump input
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }

            // Mouse look
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            // Rotate player left/right
            transform.Rotate(Vector3.up * mouseX);

            // Rotate camera up/down
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevent over-rotation
            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 10f;
        }
        else
        {
            moveSpeed = 5f;
        }

        if (Input.GetKey(KeyCode.E))
        {
            if (distanceondoor < 5f)
            {
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
}