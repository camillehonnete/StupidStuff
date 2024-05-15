using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Player;
using UnityEngine;

public class Chair : MyObject
{
    [SerializeField] private Transform camPos;
    [SerializeField] private Transform outsideChairPos;
    [SerializeField]private float lookSpeed = 2f;
  
    private float baseLookSpeed;
    private BoxCollider _boxCollider;
    protected override void Start()
    {
        base.Start();
        _boxCollider = GetComponent<BoxCollider>();
    }


    public override void Interact()
    {
        base.Interact();
        

        if (hasInteracted)
        {
            //TODO Logic to enter the Locker
            baseLookSpeed = PlayerController.Instance.lookSpeed;
            PlayerController.Instance.lookSpeed = lookSpeed;
            
            PlayerController.Instance.ChangeState(PlayerState.Idle);
            PlayerController.Instance.characterController.enabled = false;
            //PlayerController.Instance.canMove = false;
        
            PlayerController.Instance.transform.DOMove(camPos.position, 1);
            PlayerController.Instance.transform.DORotate(transform.rotation.eulerAngles, 1);

            _boxCollider.center = new Vector3(0,0,3);
        }
        else
        {
            //TODO Logic to exit the Locker
            PlayerController.Instance.lookSpeed = baseLookSpeed;
        
            PlayerController.Instance.transform.DOMove(outsideChairPos.position, 1).OnComplete((() => 
                OnCompleteExit()));

            _boxCollider.center = Vector3.zero;
        }
    }
    private void OnCompleteExit()
    {
        PlayerController.Instance.ChangeState(PlayerState.Idle);
        PlayerController.Instance.characterController.enabled = true;
        PlayerController.Instance.canMove = true;
    }
}
