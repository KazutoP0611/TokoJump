using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject player;
    public Vector3 cameraOffSet;

    Vector3 cameraVec;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraVec = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        cameraVec.z = cameraOffSet.z;
        cameraVec.x = player.transform.position.x;
        cameraVec.y = player.transform.position.y + cameraOffSet.y;

        transform.position = cameraVec;

        //transform.LookAt(player.transform.position);
    }
}
