using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private float health = 100f;
    [SerializeField] private int scoreValue = 100;

    [Header("Projectile")]
    [SerializeField] private float minTimeBetweenShots = .3f;
    [SerializeField] private float maxTimeBetweenShots = 3f;
    [SerializeField] private float shotCounter;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private GameObject bullet = null;

    [Header("Effects")]
    [SerializeField] private GameObject explosion = null;
    [SerializeField] private float explosionDuration = .4f;

    [Header("Audio")]
    [SerializeField] private AudioClip killedSound = null;
    [SerializeField] [Range(.0f, 1f)] private float killedSoundVolume = 0.7f;
    [SerializeField] private AudioClip shoothingSound = null;
    [SerializeField] [Range(.0f, 1f)] private float shottingSoundVolume = 1f;

    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        AudioSource.PlayClipAtPoint(shoothingSound, Camera.main.transform.position, shottingSoundVolume);
        GameObject fireLaser = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
        fireLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed * -1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer == null)
        {
            return;
        }

        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();

        damageDealer.Hit();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject explosionEffect = Object.Instantiate(explosion, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(killedSound, Camera.main.transform.position, killedSoundVolume);
        Destroy(explosionEffect, explosionDuration);
        Destroy(gameObject);

        FindObjectOfType<GameState>().AddToScore(scoreValue);
    }
}
