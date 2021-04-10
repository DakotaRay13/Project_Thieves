using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Manager_Alex : MonoBehaviour
{
    public AudioClip walk, chain, heavySwing, destroy, block, hit, death;

    public AudioSource source1;
    public AudioSource source2;

    public void PlayAudio(string clip)
    {
        switch (clip)
        {
            case ("walk"):
                {
                    if(!GetComponentInParent<Player_Alex>().isBlocking) source1.clip = walk; 
                    else return; break;
                }
            case ("chain"):         source1.clip = chain; break;
            case ("heavySwing"):    source1.clip = heavySwing; break;
            case ("boom"):          source1.clip = destroy; break;
            case ("block"):         source1.clip = block; break;
            case ("hit"):           source1.clip = hit; break;
            case ("death"):         source1.clip = death; break;
        }

        source1.Play();
    }

    public void PlayAudio2(string clip)
    {
        switch (clip)
        {
            case ("walk"):
                {
                    if (!GetComponentInParent<Player_Alex>().isBlocking) source2.clip = walk;
                    else return; break;
                }
            case ("chain"):         source2.clip = chain; break;
            case ("heavySwing"):    source2.clip = heavySwing; break;
            case ("boom"):          source2.clip = destroy; break;
            case ("block"):         source2.clip = block; break;
            case ("hit"):           source2.clip = hit; break;
            case ("death"):         source2.clip = death; break;
        }

        source2.Play();
    }
}
