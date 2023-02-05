using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject enemyBullet;
    public Transform []spawnPoint;
    // public Transform spawnPoint2;
    public float bulletSpawnTime = 1f;
    public GameObject Flash;
    private Animation explosion;
    public GameObject enemyExplosionPrefab;
    public GameObject damageEffect1;
    public float health = 10f;
    float barSize = 1f;
    float damage = 0;
    public GameObject CoinPrefab;
    public AudioClip bulletSound;
    public AudioClip damageSound;
    public AudioClip explosionSound;
    public AudioSource audioSource;

    public HealthBar healthbar;
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Flash.SetActive(false);
        StartCoroutine(Shoot());
        // explosion = GameObject.GetComponent<Animation>();
        // explosion.Play("Explosion");
        damage = barSize/health;
    }

    // Update is called once per frame
    void Update()
    {
                transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

     private void OnTriggerEnter2D(Collider2D collision)
     {
        if(collision.tag == "PlayerBullet")
        {
            audioSource.PlayOneShot(damageSound);
            DamageHealthbar();
            Destroy(collision.gameObject);
            GameObject damageVfx1 = Instantiate(damageEffect1, collision.transform.position, Quaternion.identity);
            Destroy(damageVfx1, 0.05f);
            if (health <= 0)
            {
               AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, 0.5f);
               Destroy(gameObject);
               GameObject explosionAnimation = Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
               Destroy(explosionAnimation, 0.4f); 
               Instantiate(CoinPrefab, transform.position, Quaternion.identity)  ;
            }
        }
     }

     void DamageHealthbar()
     {
        if (health > 0 )
        {
            health -= 1;
            barSize = barSize - damage;
            healthbar.SetSize(barSize);
        }
     }

    void Fire()
    {
        for(int i = 0; i < spawnPoint.Length; i++)
        {
        Instantiate(enemyBullet, spawnPoint[i].position, Quaternion.identity);
        }
        // Instantiate(enemyBullet, spawnPoint2.position, Quaternion.identity);
    }
    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(bulletSpawnTime);
            Fire();
            audioSource.PlayOneShot(bulletSound, 0.5f);
            Flash.SetActive(true);
            yield return new WaitForSeconds(0.04f);
            Flash.SetActive(false);
        }
    }
}
