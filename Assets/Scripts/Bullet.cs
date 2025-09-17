using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] AudioClip explodeSoundClip;

    [Header("Effect Settings")]
    public GameObject explosionPrefab;
    public float destroyEffectInSecs;

    void Start()
    {
        Invoke("DestroyBullet", 3.0f);
    }

    private void Update()
    {
        CheckIfOutOfScreen();
    }


    //private void DestroyBullet()
    //{
    //    Destroy(gameObject);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        AudioSource.PlayClipAtPoint(explodeSoundClip, transform.position);

        GameObject explodeFX = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explodeFX, destroyEffectInSecs);

        if (collision.gameObject.CompareTag("Breakable") || collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }

        Destroy(gameObject);
    }

    private void CheckIfOutOfScreen()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        bool isOffscreen = pos.x <= 0 || pos.x >= Screen.width || pos.y <= 0 || pos.y >= Screen.height;
        if (isOffscreen)
        {
            Destroy(gameObject);
        }
    }
}
