using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    public static CamScript instance { get; private set; }
    [SerializeField] private Transform camTarget;
    [SerializeField] private Transform InCombat;
    //Lerp de position et rotation
    [SerializeField] private float pLerp = .02f;
    [SerializeField] private float rLerp = .02f;

    public bool isLerping = false;
    private void Awake()
    {
        instance = this;
        Vector3 firstSpot = transform.localPosition;
        Quaternion firstRotation = transform.localRotation;
    }

    private void Update()
    {
        OnCombat();
    }
    public void OnCombat()
    {
        if (isLerping)
        {
            //Use move toward instead ?
            transform.localPosition = Vector3.Lerp(transform.localPosition, InCombat.position, pLerp);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, InCombat.rotation, rLerp);
        }
        else
        {
            return;
        }
        
    }

    
}
