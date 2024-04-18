using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class MyObject : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Interact()
    {
        Debug.Log("Interaction");
    }
}
