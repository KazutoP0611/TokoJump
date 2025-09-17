using UnityEngine;

public class CreateBullet : MonoBehaviour
{
    public KeyCode shotKey = KeyCode.Mouse0;

    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public float firePower = 15.0f;
    public float yOffSet = 0.5f;

    [Header("Audio Settings")]
    [SerializeField] AudioSource bulletSource;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireBullet();
        }
    }

    public void FireBullet()
    {
        Vector3 pos = transform.position + transform.forward * 0.5f;
        pos.y += yOffSet;

        GameObject bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);

        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * firePower, ForceMode.Impulse);

        bulletSource.Play(0);
    }
}
