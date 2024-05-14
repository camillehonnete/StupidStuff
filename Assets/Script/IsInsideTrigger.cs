using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class IsInsideTrigger : MonoBehaviour
{
    [Header("GizmoColor")] 
    [SerializeField] private Color32 cubeColor;
    [SerializeField] private Color32 cubeLineColor;
    
    protected bool isInside = false;
    protected BoxCollider _boxCollider;

    protected void Awake()
    {
        _boxCollider ??= transform.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (isInside)
        {
            Inside();
        }
        else
        {
            Outside();
        }
    }
    
    protected virtual void Inside(){}
    protected virtual void Outside(){}
    
    
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = true;
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = false;
        }
    }
    
#if UNITY_EDITOR

    protected virtual void OnDrawGizmos()
    {
        _boxCollider = GetComponent<BoxCollider>();
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontSize = 15;
        style.fontStyle = FontStyle.Bold;
        Handles.Label(transform.position, transform.name, style);


        var scale = transform.lossyScale;
        
        Gizmos.color = cubeColor;
        Gizmos.DrawCube(transform.position, _boxCollider.bounds.size);
        
        Gizmos.color = cubeLineColor;
        Gizmos.DrawWireCube(transform.position, _boxCollider.bounds.size);
    }
#endif
}
