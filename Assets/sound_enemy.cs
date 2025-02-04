using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_enemy : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    public Transform player;

    private float distanceThreshold = 3f;
    private float cooldownDuration = 3f;
    private float cooldownTimer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        cooldownTimer = cooldownDuration;
    }

    private void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        CheckDistance();
    }

    private void CheckDistance()
    {
        if (cooldownTimer <= 0)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance < distanceThreshold && !audioSource.isPlaying)
            {
                audioSource.Play();
                cooldownTimer = cooldownDuration;
            }
        }
    }
}