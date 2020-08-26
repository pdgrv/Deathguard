using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private List<AudioClip> _clips;

    public void PlaySound(string SoundName)
    {
        _audio.pitch = 1f;

        switch (SoundName)
        {
            case "Swing":
                _audio.pitch = Random.Range(0.7f, 1.3f);
                _audio.PlayOneShot(_clips[0]);
                break;
            case "Use":
                _audio.PlayOneShot(_clips[1]);
                break;
            case "Lvlup":                
                _audio.PlayOneShot(_clips[2]);
                break;
            case "ApplyDamage":
                _audio.pitch = Random.Range(0.7f, 1.3f);
                _audio.PlayOneShot(_clips[3]);
                break;
            case "Die":
                _audio.PlayOneShot(_clips[4]);
                break;
        }
    }
}
