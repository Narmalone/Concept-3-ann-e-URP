using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXManager : MonoBehaviour
{
    public static VFXManager instance { get; private set; }
    public List<VisualEffect> m_effects;
    private bool isPlaying = false;
    private void Awake()
    {
        instance = this;
    }

    public void PlayVfx(int spellID)
    {
        m_effects[spellID].GetComponent<VisualEffect>().SendEvent("OnPlay");
        isPlaying = true;
    }
}
