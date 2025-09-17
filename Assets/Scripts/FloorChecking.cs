using UnityEngine;

public class FloorChecking : MonoBehaviour
{
    [SerializeField] LayerMask floorLayer;

    public bool jumping {
        get { return v_jumping; }
        set { v_jumping = value; }
    }

    bool v_jumping = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Mathf.Log(floorLayer, 2))
        {
            //Debug.LogError(other.gameObject.name);
            v_jumping = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == Mathf.Log(floorLayer, 2))
        {
            //Debug.LogError(other.gameObject.name);
            v_jumping = true;
        }
    }
}
