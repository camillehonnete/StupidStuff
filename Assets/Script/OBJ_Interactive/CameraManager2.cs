using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager2 : MonoBehaviour
{

    public static CameraManager2 Instance;
    public Transform holdingPoint;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
