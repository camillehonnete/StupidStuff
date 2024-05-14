using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class LightEnabler : MonoBehaviour
{
    private Light[] _lights;
    [SerializeField] private float desiredLightIntensity = 30f;
    [SerializeField] private float LightIntensityMultiplicator = 2f;

    private float timer = 0f;
    private bool isInside = false;
    
    private void Start()
    {
        _lights = transform.GetComponentsInChildren<Light>();
    }
    
    private void Update()
    {
        if (isInside)
        {
            foreach (var light in _lights)
            {
                if (light.intensity < desiredLightIntensity)
                {
                    light.intensity += Time.deltaTime * LightIntensityMultiplicator;
                }
            }
        }
        else
        {
            foreach (var light in _lights)
            {
                if (light.intensity > 0)
                {
                    light.intensity -= Time.deltaTime * LightIntensityMultiplicator;
                }
            }
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = false;
        }
    }
    


#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontSize = 15;
        style.fontStyle = FontStyle.Bold;
        Handles.Label(transform.position, transform.name, style);


        var scale = transform.localScale;
        
        Gizmos.color = new Color32(255,0,0,100);
        Gizmos.DrawCube(transform.position, new Vector3(scale.x, scale.x, scale.z));
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(scale.x, scale.x, scale.z));
    }
#endif
}
