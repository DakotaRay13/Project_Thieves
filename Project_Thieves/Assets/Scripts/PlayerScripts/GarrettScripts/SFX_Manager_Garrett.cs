using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Manager_Garrett : MonoBehaviour
{
    public AudioClip walk, nailGun, boom, rack1, rack2, hit, death;

    public AudioSource source;
    public AudioSource source2;

    public void PlayAudio(string clip)
    {
        switch (clip)
        {
            case ("walk"): source.clip = walk; break;
            case ("nailGun"): source.clip = nailGun; break;
            case ("boom"): source.clip = boom; break;
            case ("rack1"): source.clip = rack1; break;
            case ("rack2"): source.clip = rack2; break;
            case ("hit"): source.clip = hit; break;
            case ("death"): source.clip = death; break;
        }

        source.Play();
    }

    public void PlayAudio2(string clip)
    {
        switch (clip)
        {
            case ("walk"): source2.clip = walk; break;
            case ("nailGun"): source2.clip = nailGun; break;
            case ("boom"): source2.clip = boom; break;
            case ("rack1"): source2.clip = rack1; break;
            case ("rack2"): source2.clip = rack2; break;
            case ("hit"): source2.clip = hit; break;
            case ("death"): source2.clip = death; break;
        }

        source2.Play();
    }
}
