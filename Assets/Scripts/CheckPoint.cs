using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] Renderer checkPointObjectRenderer;
    [SerializeField] Material redCheckPoint;
    [SerializeField] Material greenCheckPoint;
    [SerializeField] AudioSource audioSouce;
    [SerializeField] Animator anim;
    [SerializeField] bool faceRightWhenSpawn = true;

    Collider myCollider;
    bool pointChecked = false;

    private void Start()
    {
        myCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!pointChecked)
            {
                pointChecked = true;

                anim.SetTrigger("Check");
                myCollider.enabled = false;
                checkPointObjectRenderer.material = greenCheckPoint;
                Respawn respawnScript = FindFirstObjectByType<Respawn>();
                if (respawnScript)
                {
                    respawnScript.SetNewSpawnPoint(faceRightWhenSpawn);
                }

                audioSouce.Play(0);
                myCollider.enabled = false;
            }
        }
    }
}
