using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    public static CamScript instance { get; private set; }
    [SerializeField] private Animation motion;
    private void Awake()
    {
        instance = this;
        motion = GetComponent<Animation>();
    }
    public void OnCombat()
    {
        AnimationManager.instance.PlayAnim(motion, WrapMode.Once);
    }

    public void OnCombatEnd()
    {
        motion.GetClip("EndCombat");
        AnimationManager.instance.PlayAnim(motion, WrapMode.Once);
    }

    
}
