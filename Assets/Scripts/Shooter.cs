using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [Header("Player fire tuning")]
    [SerializeField] float baseFiringRate = 0.4f;
    
    [Header("AI fire tuning")]
    [SerializeField] float aiBaseFireRate = 2f;
    [SerializeField] float aiFireRateVariance = 0f;
    [SerializeField] float aiMinFireRate = 0.1f;
    [SerializeField] bool useAI;
    [HideInInspector] public bool isFiring = false;

    AudioPlayer audioPlayer;
    Coroutine firingCoroutine;
    bool waitingToShoot = false;

    void Awake() 
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();    
    }

    void Start()
    {
        if(useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();    
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null && !waitingToShoot)
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinously()
    {
        while (true) 
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, transform.rotation);

            Rigidbody2D rbProjectile = instance.GetComponent<Rigidbody2D>();
            if (rbProjectile != null)
            {
                rbProjectile.velocity = transform.up * projectileSpeed;
            }

            Destroy(instance, projectileLifetime);

            if (useAI) 
            { 
                baseFiringRate = SetAIRandomFiringRate();
                audioPlayer.PlayEnemyMainShootingClip(); 
            }
            else
            {
                audioPlayer.PlayPlayerMainShootingClip();
            }

            waitingToShoot = true;
            StartCoroutine(FireController());
            yield return new WaitForSeconds(baseFiringRate);
        }
    }

    IEnumerator FireController()
    {
        yield return new WaitForSeconds(baseFiringRate);
        waitingToShoot = false;
    }

    float SetAIRandomFiringRate()
    {
        float aiFireRate = Random.Range(
            aiBaseFireRate - aiFireRateVariance,
            aiBaseFireRate + aiFireRateVariance
        );

        return Mathf.Clamp(aiFireRate, aiMinFireRate, float.MaxValue);
    }
}
