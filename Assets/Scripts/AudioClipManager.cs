using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private List<SoundClip> _clips;

    public void PlaySound(string SoundName)
    {
        _audio.pitch = 1f;

        foreach (SoundClip clip in _clips)
        {
            if (clip.Name == SoundName)
            {
                if (clip.RandomPitch)
                    _audio.pitch = Random.Range(0.7f, 1.3f);
                _audio.PlayOneShot(clip.AudioClip);
            }
        }
    }
}

[System.Serializable]
public class SoundClip
{
    public string Name;
    public AudioClip AudioClip;
    public bool RandomPitch;
}
