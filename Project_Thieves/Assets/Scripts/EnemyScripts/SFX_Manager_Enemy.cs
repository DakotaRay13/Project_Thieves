using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Manager_Enemy : MonoBehaviour
{
    public AudioClip hit;

    public AudioSource source;

    public void PlayAudio(string clip)
    {
        switch (clip)
        {
            case ("hit"): source.clip = hit; break;
        }

        source.Play();
    }
}
