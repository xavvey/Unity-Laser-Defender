using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int pointsOnAIdeath = 10;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCamShake;
    LevelManager levelManager;
    ScoreKeeper scoreKeeper;
    CameraShake cameraShake;
    AudioPlayer audioPlayer;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            damageDealer.Hit();
        }    
    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(
                hitEffect,
                transform.position,
                Quaternion.identity
            );
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            audioPlayer.PlayDamageClip();
        }
    }

    private void Die()
    {
        if (!isPlayer)
        {
            scoreKeeper.ModifyScore(pointsOnAIdeath);
        }
        else
        {
            levelManager.LoadGameOver();
        }

        audioPlayer.PlayDestroyClip();
        Destroy(gameObject);
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCamShake)
        {
            cameraShake.Play(); 
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
