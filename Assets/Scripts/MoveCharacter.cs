using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    [SerializeField] float speed = 3;
    [SerializeField] float jump = 10;

    Rigidbody rb;
    bool jumping;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            rb.AddForce(Vector3.right * horizontal * speed);
        }

        if (Input.GetKey(KeyCode.Space) && !jumping)
        {
            rb.AddForce(Vector3.up * jump, ForceMode.Acceleration);
            jumping = true;
        }

        if (rb.GetRelativePointVelocity(gameObject.transform.position).y <= 0)
        {
            jumping = false;
        }
    }
}
