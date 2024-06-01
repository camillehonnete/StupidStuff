using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class Valise : MyObject
{
    [SerializeField] private float transitionTime;
    [SerializeField] private int holdingPointIndex;
    
    public bool isOwned;
    [HideInInspector] public Collider _collider;
    void Start()
    {
        _collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOwned)
        {
            transform.position = Vector3.Lerp(transform.position, PlayerController.Instance.holdingPoints[holdingPointIndex].position, Time.deltaTime * transitionTime);
            transform.forward = Vector3.Lerp(transform.forward, PlayerController.Instance.holdingPoints[holdingPointIndex].forward, Time.deltaTime * transitionTime);
        }
    }

    public override void Interact()
    {
        base.Interact();
        isOwned = true;
        _collider.enabled = false;
        PlayerController.Instance.valiseOwning.Add(this);
        Debug.Log("Avec la valise");
    }
}
