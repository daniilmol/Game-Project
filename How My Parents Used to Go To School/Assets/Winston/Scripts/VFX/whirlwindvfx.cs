using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class whirlwindvfx : MonoBehaviour
{
    public ParticleSystem whirlwind;

    void Start()
    {
        whirlwind = GetComponent<ParticleSystem>();
    }

    public void playWhirlwind() {
        whirlwind.Play();
    }
}

