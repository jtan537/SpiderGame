using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Cameron Hadfield
// Created: 6/3/21
// Last modified: 6/3/21
// A class for managing the player's crosshair
public class PlayerCrosshair : MonoBehaviour
{
    [SerializeField]
    private Transform CrosshairForward;

    public Vector3 GetCrosshairForward()
    {
        return CrosshairForward.forward;
    }
    
}
