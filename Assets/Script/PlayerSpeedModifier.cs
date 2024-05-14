using System.Collections;
using System.Collections.Generic;
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
            baseWalkSpeed = FirstPersonController.Instance.MoveSpeed;
            FirstPersonController.Instance.MoveSpeed = newWalkSpeed;
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FirstPersonController.Instance.MoveSpeed = baseWalkSpeed;
        }
    }
}
