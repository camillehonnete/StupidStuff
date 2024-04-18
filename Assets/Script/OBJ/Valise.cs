using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valise : MyObject
{
    public float transitionTime;
    
    private bool isOwned;
    private Collider _collider;
    void Start()
    {
        _collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOwned)
        {
            transform.position = Vector3.Lerp(transform.position, CameraManager.Instance.holdingPoint.position, Time.deltaTime * transitionTime);
            transform.forward = Vector3.Lerp(transform.forward, CameraManager.Instance.holdingPoint.forward, Time.deltaTime * transitionTime);
        }
    }

    public override void Interact()
    {
        base.Interact();
        isOwned = true;
        _collider.enabled = false;
        Debug.Log("Avec la valise");
    }
}
