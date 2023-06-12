using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoMenu : MonoBehaviour
{
    public AudioSource enemyAudioSource;
    public AudioClip[] growlAudioClips;

    // Start is called before the first frame update
    void Start()
    {
        enemyAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyAudioSource.clip = growlAudioClips[Random.Range(0, growlAudioClips.Length)];
        enemyAudioSource.Play();
    }
}
