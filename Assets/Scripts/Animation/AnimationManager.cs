using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void PlayAnim(Animation playMotion, WrapMode wrapMode)
    {
        playMotion.wrapMode = wrapMode;
        playMotion.Play();
    }
}
