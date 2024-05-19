using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class LightEnabler : IsInsideTrigger
{
    [Space] 
    [SerializeField] private float desiredLightIntensity = 30f;
    [SerializeField] private float LightIntensityMultiplicator = 2f;
    private Light[] _lights;
    
    private void Start()
    {
        _lights = transform.GetComponentsInChildren<Light>();
        foreach (var light in _lights)
        {
            light.intensity = 0;
        }
    }

    protected override void Inside()
    {
        base.Inside();
        foreach (var light in _lights)
        {
            if (light.intensity < desiredLightIntensity)
            {
                light.intensity += Time.deltaTime * LightIntensityMultiplicator;
            }
        }
    }

    protected override void Outside()
    {
        base.Outside();
        foreach (var light in _lights)
        {
            if (light.intensity > 0)
            {
                light.intensity -= Time.deltaTime * LightIntensityMultiplicator;
            }
        }
    }
    
}
