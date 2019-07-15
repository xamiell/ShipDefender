using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(5f, 10f)] [SerializeField] float moveSpeed = 8.5f;
    [SerializeField] GameObject playerLaser;
    [SerializeField] float laserSpeed = 7f;
    [SerializeField] Joystick joystick;
    [SerializeField] AudioClip fireSound;
    
    [SerializeField] GameObject explosionVFX;
    [SerializeField] AudioClip explosionSound;

    private Rigidbody2D m_rigidbody;
    private Vector2 m_movePos;
    private float m_laserSpeed;
    private float m_moveSpeed;
    private Vector3 m_mainCameraPosition;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_laserSpeed = laserSpeed;
        m_moveSpeed = moveSpeed;
        m_mainCameraPosition = Camera.main.transform.position;

        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        m_movePos = new Vector2( joystick.Horizontal, joystick.Vertical );
    }

    public void Shoot()
    {
        ShootProjectile();
        AudioSource.PlayClipAtPoint(fireSound, m_mainCameraPosition);
    }

    private void FixedUpdate()
    {
        m_rigidbody.MovePosition((Vector2)transform.position + (m_movePos * m_moveSpeed * Time.fixedDeltaTime));
    }

    void ShootProjectile()
    {
        GameObject laser = Instantiate(playerLaser, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, m_laserSpeed * 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy_Laser") ||
            collision.gameObject.tag.Equals("Enemy"))
        {
            gameManager.UpdateHealth();

            if (gameManager.GetPlayerHeath() <= 0)
            {
                Destroy(gameObject);

                var explosion = Instantiate(explosionVFX, transform.position, Quaternion.identity);
                PlayExplosionSound();

                Destroy(explosion, 0.5f);
            }
        }
    }

    private void PlayExplosionSound()
    {
        AudioSource.PlayClipAtPoint(explosionSound, m_mainCameraPosition);
    }
}
