using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform startTransform;
    [SerializeField] Transform endTransform;
    [SerializeField] float speed;
    [SerializeField] Transform platformParentObj;

    bool movingToward = true;
    Vector3 targetPoint;

    private void Start()
    {
        movingToward = true;
        targetPoint = endTransform.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, Time.deltaTime * speed);

        if (transform.position == targetPoint)
        {
            SetGotoVector();
        }
    }

    private void SetGotoVector()
    {
        if (movingToward)
        {
            targetPoint = startTransform.position;
            movingToward = false;
        }
        else
        {
            targetPoint = endTransform.position;
            movingToward = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(platformParentObj, true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null, true);
        }
    }
}
