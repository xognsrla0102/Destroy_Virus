using System;
using System.Collections.Generic;
using UnityEngine;

public enum Sound_Effect
{
    SHOT,
    PRESS_BUTTON,
    GET_ITEM,
    EXPLOSION,
    HIT
}


public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
            }
            return instance;
        }
     }

    [SerializeField] private List<AudioClip> sfxs;

    public void PlaySound(Sound_Effect soundType)
    {
        GameObject go = new GameObject("sound");

        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.volume = 0.1f;
        audioSource.clip = sfxs[(int)soundType];
        audioSource.Play();

        Destroy(go, audioSource.clip.length);
    }
}
