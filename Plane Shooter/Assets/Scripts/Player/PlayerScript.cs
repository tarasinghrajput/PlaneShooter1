using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Variable declaration
    public GameObject explosion;
    public PlayerHealthbarScript playerHealthbar;
    public float speed = 10f;
    public float padding = 0.8f;
    public GameController gameController;
    float minX;
    float maxX;
    float minY;
    float maxY;
    public float health = 20f;
    public GameObject damageEffect2;
    float barFillAmount = 1f;
    public CoinCount coinCountScript;
    float damage = 0;
    public AudioSource audioSource;
    public AudioClip damageSound;
    public AudioClip explosionSound;
    public AudioClip coinSound;
    // Start is called before the first frame update
    void Start()
    {
        // calling FindBoundaries();
        FindBoundaries();
        damage = barFillAmount / health;
    }
    // declaring the FindBoundaries() here
    void FindBoundaries()
    {
        // this gets the main camera into the gameCamera variable
        Camera gameCamera = Camera.main;
        // ViewportToWorldPoint() convert the Viewport values to WorldSpace value
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    // Update is called once per frame
    void Update()
    {
        // PC INPUT system
        // float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        // float newXpos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);
        // //   transform.position = new Vector2(newXpos, transform.position.y);

        // float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        // float newYpos = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);
        // transform.position = new Vector2(newXpos, newYpos);
        if (Input.GetMouseButton(0))
        {
            Vector2 newPos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            transform.position = Vector2.Lerp(transform.position, newPos, 10 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            audioSource.PlayOneShot(damageSound, 0.5f);
            DamagePlayerHealthbar();
            Destroy(collision.gameObject);
            GameObject damageVfx2 = Instantiate(damageEffect2, collision.transform.position, Quaternion.identity);
            Destroy(damageVfx2, 0.05f);
            if (health <= 0)
            {
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position , 0.5f);
            Destroy(gameObject);
            GameObject blast = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(blast, 2f);
            gameController.GameOver();
            }
        }

        if(collision.gameObject.tag == "Coin")
        {
            audioSource.PlayOneShot(coinSound, 0.5f);
            Destroy(collision.gameObject);
            coinCountScript.AddCount();
        }
    }

    void DamagePlayerHealthbar()
    {
        if (health > 0)
        {
            health -= 1;
            barFillAmount = barFillAmount - damage;
            playerHealthbar.SetAmount(barFillAmount);
        }              
    }
}
