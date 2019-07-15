using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyLaser;
    [SerializeField] GameObject explosionVFX;
    [Range(-2f, -6f)] [SerializeField] float moveSpeed = -2f;
    [SerializeField] float shootRate = 1f;
    [Range(-3f, -7f)] [SerializeField] float laserSpeed = -6f;
    [SerializeField] bool canShoot = true;
    [SerializeField] AudioClip explosionSound;

    private GameManager gameManager;

    private Vector3 m_mainCameraPosition;
    private Rigidbody2D m_rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_mainCameraPosition = Camera.main.transform.position;
        m_rigidbody2D.velocity = transform.TransformDirection(new Vector3(0, moveSpeed, 0));
        gameManager = FindObjectOfType<GameManager>();

        StartCoroutine(Shoot());
    }

    private void PlayDeadSound()
    {
        AudioSource.PlayClipAtPoint(explosionSound, m_mainCameraPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player_Laser")
        {
            Destroy(gameObject);

            gameManager.UpdateScore();

            var explosion = Instantiate(explosionVFX, transform.position, Quaternion.identity) as GameObject;
            PlayDeadSound();

            Destroy(explosion, 0.5f);
        }
    }

    IEnumerator Shoot()
    {
        while (canShoot)
        {
            yield return new WaitForSeconds(shootRate);
            var laser = Instantiate(enemyLaser, transform.position, transform.rotation) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(new Vector3(0, laserSpeed, 0));
        }
    }
}
