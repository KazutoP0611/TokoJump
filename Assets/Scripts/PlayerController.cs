using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public Gameplay gameplayManager;

    [Header("Animation Settings")]
    public Animator anim;

    [Header("Movement Settings")]
    public float speed = 5.0f;

    [Header("Jump Settings")]
    public float jumpPow = 5.0f;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode jumpKey = KeyCode.Space;
    public AudioSource jumpAudioSource;

    [SerializeField] 

    [Header("Ground Settings")]
    public float GroundedOffset = -0.14f;
    public float GroundedRadius = 0.5f;
    [SerializeField] LayerMask GroundLayers;

    Rigidbody rb;
    int direction = 0;
    int facing = 1;
    bool Grounded = false;

    const string SHELL_TAG_STRING = "Shell";

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GroundedCheck();
        MovementCheck();
        JumpCheck();
    }

    private void MovementCheck()
    {
//#if UNITY_5_6_OR_NEWER
//        if (Input.GetKey(rightKey))
//        {
//            facing = 1;
//            direction = 1;
//        }
//        else if (Input.GetKey(leftKey))
//        {
//            facing = -1;
//            direction = -1;
//        }
//        else
//        {
//            direction = 0;
//        }
//#else
//        if (Input.GetAxisRaw("Horizontal") > 0f)
//        {
//            facing = 1;
//            direction = 1;
//        }
//        else if (Input.GetAxisRaw("Horizontal") < 0f)
//        {
//            facing = -1;
//            direction = -1;
//        }
//        else
//        {
//            direction = 0;
//        }
//#endif

        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            facing = 1;
            direction = 1;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            facing = -1;
            direction = -1;
        }
        else
        {
            direction = 0;
        }
    }

    private void JumpCheck()
    {
        //#if UNITY_5_6_OR_NEWER
        //        if (Input.GetKeyDown(jumpKey) && Grounded)
        //        {
        //            rb.AddForce(Vector3.up * jumpPow, ForceMode.Impulse);
        //        }
        //#else
        //        if (Input.GetButtonDown("Jump") && Grounded)
        //        {
        //            rb.AddForce(Vector3.up * jumpPow, ForceMode.Impulse);
        //        }
        //#endif

        if (Input.GetButtonDown("Jump") && Grounded)
        {
            anim.SetBool("Jump", true);
            rb.AddForce(Vector3.up * jumpPow, ForceMode.Impulse);
            jumpAudioSource.Play(0);
            //transform.parent = null;
        }

        if (!Grounded)
        {
            anim.SetBool("Jump", false);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 moveVec = new Vector3(direction, 0, 0) * speed;

        transform.rotation = Quaternion.Euler(new Vector3(0, facing * 90, 0));

        if (!Grounded)
            transform.position += (moveVec * Time.deltaTime) / 2.0f;
        else
            transform.position += (moveVec * Time.deltaTime);

        if (Grounded)
            anim.SetBool("Running", moveVec.magnitude > 0);
    }

    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers);
    }

    public void SetFacingRight(bool faceRight)
    {
        if (faceRight)
            facing = 1;
        else
            facing = -1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(SHELL_TAG_STRING))
        {
            gameplayManager.UpdateShellCount(1);
            Destroy(other.gameObject);
        }
    }
}
