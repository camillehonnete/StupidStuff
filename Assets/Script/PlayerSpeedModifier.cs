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

    [Space] 
    [SerializeField] private float newSprintSpeed;
    private float baseSprintSpeed;
    
    
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            baseWalkSpeed = PlayerController.Instance.walkSpeed;

            baseSprintSpeed = PlayerController.Instance.runSpeed;

            PlayerController.Instance.runSpeed = newSprintSpeed;
            PlayerController.Instance.walkSpeed = newWalkSpeed;
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.Instance.walkSpeed = baseWalkSpeed;
            PlayerController.Instance.runSpeed = baseSprintSpeed;
        }
    }
}
