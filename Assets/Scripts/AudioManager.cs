using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AM;

    public AudioSource enemyDies;
    public AudioSource gameOver;
    public AudioSource enemyHit;
    public AudioSource shoot;

    void Start()
    {
        AM = this;
    }
}
