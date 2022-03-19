using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip playerShootingClip;
    [SerializeField] AudioClip enemyShootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 0.5f;
    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField] AudioClip destroyClip;
    [SerializeField] [Range(0f, 1f)] float damageVolume = 0.5f;

    public void PlayPlayerMainShootingClip()
    {
        PlayClip(playerShootingClip, shootingVolume);
    }

    public void PlayEnemyMainShootingClip()
    {
        PlayClip(enemyShootingClip, shootingVolume);
    }

    public void PlayDamageClip()
    {
        PlayClip(damageClip, damageVolume);
    }

    public void PlayDestroyClip()
    {
        PlayClip(destroyClip, damageVolume);
    }

    void PlayClip(AudioClip audioClip, float volume)
    {
        if (audioClip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(audioClip, cameraPos, volume);
        }
    }
}
