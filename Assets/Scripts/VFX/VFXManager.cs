using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public void LaunchParticle(ParticleSystem particleToCast, Spell fromSpell)
    {
        particleToCast.Play();
    }
}
