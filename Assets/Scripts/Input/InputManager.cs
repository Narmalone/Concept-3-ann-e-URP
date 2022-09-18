using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }

    public void LaunchSpellButton(Input value)
    {
        Debug.Log("fire");
    }
}
