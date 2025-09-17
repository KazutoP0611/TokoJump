using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Respawn : MonoBehaviour
{
    [SerializeField] Transform[] respawnPoints;
    [SerializeField] Goal goal;

    const string playerTag = "Player";
    Transform respawnPoint;
    int spawnPointCount;

    bool facingRight = true;

    private void Start()
    {
        respawnPoint = respawnPoints[0];
        spawnPointCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            //other.transform.position = respawnPoint.position;
            other.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            SetStartObj(other.gameObject);
            goal?.CloseText();
        }
    }

    public void SetNewSpawnPoint(bool faceRight)
    {
        spawnPointCount += 1;
        if (spawnPointCount < respawnPoints.Length)
        {
            respawnPoint = respawnPoints[spawnPointCount];
        }
        else
        {
            respawnPoint = respawnPoints[0];
        }

        facingRight = faceRight;
    }

    public void SetStartObj(GameObject obj)
    {
        obj.transform.position = respawnPoint.position;
        obj.GetComponent<PlayerController>()?.SetFacingRight(facingRight);

    }
}
