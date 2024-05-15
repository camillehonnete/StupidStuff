using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class MyObject : MonoBehaviour, IInteractable
{
    protected bool hasInteracted;
    protected virtual void  Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual void Interact()
    {
        hasInteracted = !hasInteracted;
        Debug.Log("Interaction");
    }
}
