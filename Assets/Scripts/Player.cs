using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration parameters
    [Header("Life Bar")]
    [SerializeField] private int health = 500;
    
    [Header("Player Movement")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float padding = .01f;
    
    [Header("Projectile")]
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileFiringPeriod = 0.1f;
    [SerializeField] private GameObject laser = null;
    [SerializeField] private GameObject explosion = null;
    [SerializeField] private float explosionDuration = .4f;

    [Header("Audio")]
    [SerializeField] private AudioClip killedSound = null;
    [SerializeField][Range(.0f, 1f)] private float killedSoundVolume = 1f;
    [SerializeField] private AudioClip shoothingSound = null;
    [SerializeField] [Range(.0f, 1f)] private float shottingSoundVolume = 1f;

    private Coroutine firingCoroutine = null;

    // State variables
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;
    private GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        gameState.SetCurrentHealth(health);
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
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

        if (health < 0)
            health = 0;

        damageDealer.Hit();

        gameState.SetCurrentHealth(health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(killedSound, Camera.main.transform.position, killedSoundVolume);
        GameObject explosionEffect = UnityEngine.Object.Instantiate(explosion, transform.position, transform.rotation);
        Destroy(explosionEffect, explosionDuration);
        Destroy(gameObject);

        FindObjectOfType<Level>().LoadGameOver();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireLaser());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private IEnumerator FireLaser()
    {
        while (true)
        {
            GameObject fireLaser = Instantiate(laser, transform.position, Quaternion.identity) as GameObject;
            fireLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shoothingSound, Camera.main.transform.position, shottingSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPosition = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPosition = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax); 

        transform.position = new Vector2(newXPosition, newYPosition);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
