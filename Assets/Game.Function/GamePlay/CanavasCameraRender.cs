using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanavasCameraRender : MonoBehaviour
{
    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    void Start()
    {
        canvas.worldCamera = Camera.main;
    }
}
