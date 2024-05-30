using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Player;
using UnityEngine;
using UnityEngine.Events;

public class Chair : MyObject
{
    [Header("Event")]
    [SerializeField] private UnityEvent OnInteract;
    [SerializeField] private UnityEvent OnStopInteract;
    
    [Header("Parameters")]
    [SerializeField] private Transform camPos;
    [SerializeField] private Transform outsideChairPos;
    [SerializeField]private float lookSpeed = 2f;
    
    private AudioSource audioSource;
    
    private float baseLookSpeed;
    private BoxCollider _boxCollider;
    [SerializeField]private List<BoxCollider> _listBoxCollider;
    
    protected override void Start()
    {
        base.Start();
        _boxCollider = GetComponent<BoxCollider>();

        foreach (var coll in _listBoxCollider)
        {
            coll.enabled = false;
        }
        
        audioSource = GetComponent<AudioSource>();
    }


    public override void Interact()
    {
        base.Interact();
        

        if (hasInteracted)
        {
            InteractChair();
        }
        else
        {
            DeInteractChair();
        }
    }

    private void InteractChair()
    {
        //audioSource.Play();
        
        baseLookSpeed = PlayerController.Instance.lookSpeed;
        PlayerController.Instance.lookSpeed = lookSpeed;
            
        PlayerController.Instance.ChangeState(PlayerState.Idle);
        PlayerController.Instance.characterController.enabled = false;
        //PlayerController.Instance.canMove = false;
        
        PlayerController.Instance.transform.DOMove(camPos.position, 1);
        PlayerController.Instance.transform.DORotate(transform.rotation.eulerAngles, 1);

        _boxCollider.center = new Vector3(0,0,3);
            
        foreach (var coll in _listBoxCollider)
        {
            coll.enabled = true;
        }
        
        OnInteract?.Invoke();
    }

    private void DeInteractChair()
    {
        foreach (var coll in _listBoxCollider)
        {
            coll.enabled = false;
        }
            
        //TODO Logic to exit the Locker
        PlayerController.Instance.lookSpeed = baseLookSpeed;
        
        PlayerController.Instance.transform.DOMove(outsideChairPos.position, 1).OnComplete((() => 
            OnCompleteExit()));

        _boxCollider.center = Vector3.zero;
        
        OnStopInteract?.Invoke();
    }
    
    
    
    private void OnCompleteExit()
    {
        PlayerController.Instance.ChangeState(PlayerState.Idle);
        PlayerController.Instance.characterController.enabled = true;
        PlayerController.Instance.canMove = true;
    }
}
