using System.Collections;
using System.Collections.Generic;
using Player;
using StarterAssets;
using UnityEditor;
using UnityEngine;

public class PlayerSpeedModifier : IsInsideTrigger
{
    [Space] 
    [SerializeField] private float newWalkSpeed;
    private float baseWalkSpeed;
    
    
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            baseWalkSpeed = PlayerController.Instance.walkSpeed;
            PlayerController.Instance.walkSpeed = newWalkSpeed;
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.Instance.walkSpeed = baseWalkSpeed;
        }
    }
}
