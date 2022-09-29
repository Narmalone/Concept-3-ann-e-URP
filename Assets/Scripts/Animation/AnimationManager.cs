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

    public void PlayAnim(Animation playMotion, bool isLooping)
    {
        if (isLooping)
        {
            playMotion.clip.wrapMode = WrapMode.Loop;
        }

        playMotion.Play();
    }
}
