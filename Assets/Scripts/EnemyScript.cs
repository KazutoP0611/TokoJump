using System;
using UnityEditor;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 2.0f;
    public float minX = 10.0f;
    public float maxX = 15.0f;
    public float jumpPow = 5.0f;

    private Rigidbody rb;
    private bool rightMove = true;

    [Header("Enemy Settings")]
    [SerializeField] AudioClip deathSoundClip;

    [Header("Ground Checker")]
    public float xPositionOffset = 0.5f;
    public float yPositionOffset = 0.5f;
    public float GroundedRadius = 0.2f;
    public LayerMask GroundLayers;

    [Header("Wall Checker")]
    public float wallXPositionOffset = 0.5f;
    public float wallYPositionOffset = 0.5f;
    public float wallSphereRadius = 0.2f;
    public LayerMask WallLayers;

    [Header("Respawn")]
    public Respawn respawnScript;

    bool Grounded;
    bool touchWall;
    Collider myCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myCollider = rb.GetComponent<Collider>();
    }

    void Update()
    {
        CheckGround();
        Move();
    }

    //private void FixedUpdate()
    //{
    //    Vector3 velocity = rb.linearVelocity;

    //    if (transform.position.x >= maxX)
    //    {
    //        rightMove = false;
    //    }
    //    else if (transform.position.x <= minX)
    //    {
    //        rightMove = true;
    //    }

    //    if (rightMove)
    //    {
    //        velocity.x = speed;
    //    }
    //    else
    //    {
    //        velocity.x = -speed;
    //    }
    //}

    private void CheckGround()
    {
        Vector3 spherePosition = new Vector3(transform.position.x + xPositionOffset, transform.position.y - yPositionOffset, transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers);

        Vector3 wallSpherePosition = new Vector3(transform.position.x + wallXPositionOffset, transform.position.y - wallYPositionOffset, transform.position.z);
        touchWall = Physics.CheckSphere(wallSpherePosition, wallSphereRadius, WallLayers);
    }

    private void Move()
    {
        if (Grounded)
        {
            float movingX = transform.position.x + (Time.deltaTime * speed * (rightMove ? 1 : -1));
            transform.position = new Vector3(movingX, transform.position.y, 0);

            if (touchWall)
            {
                ChangeDirection();
            }
        }
        else
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        rightMove = !rightMove;
        transform.rotation = Quaternion.Euler(0, rightMove ? 0 : 180, 0);
        xPositionOffset *= -1;
        wallXPositionOffset *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(new Vector3(transform.position.x + xPositionOffset, transform.position.y - yPositionOffset, transform.position.z), GroundedRadius);
        Gizmos.DrawSphere(new Vector3(transform.position.x + wallXPositionOffset, transform.position.y - wallYPositionOffset, transform.position.z), wallSphereRadius);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collider playerCollider = collision.gameObject.GetComponent<Collider>();
            Rigidbody playerRB = collision.gameObject.GetComponent<Rigidbody>();

            if (playerCollider.bounds.min.y > myCollider.bounds.center.y + 0.12f)
            {
                AudioSource.PlayClipAtPoint(deathSoundClip, transform.position);
                playerRB.AddForce(Vector3.up * jumpPow, ForceMode.Impulse);

                Destroy(gameObject);
            }
            else
            {
                respawnScript.SetStartObj(collision.gameObject);
            }
        }

        // Noted: This method is not that bad as well;

        //if (collision.gameObject.CompareTag("Wall"))
        //{
        //    Collider wallCollider = collision.gameObject.GetComponent<Collider>();

        //    if (wallCollider.bounds.max.x < myCollider.bounds.min.x || wallCollider.bounds.min.x > myCollider.bounds.max.x)
        //    {
        //        //Debug.LogWarning("Wall");
        //        ChangeDirection();
        //    }
        //}
    }
}
