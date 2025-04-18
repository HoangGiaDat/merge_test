using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSizeCamWidth : MonoBehaviour
{
    [SerializeField] float sceneWidth = 6;

    Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

#if UNITY_EDITOR
    private void Update()
    {
        UpdateCameraSize();
    }
#endif

    private void UpdateCameraSize()
    {
        float unitsPerPixel = sceneWidth / Screen.width;

        float desiredHalfHeight = 0.65f * unitsPerPixel * Screen.height;

        cam.orthographicSize = Mathf.Max(5, desiredHalfHeight);
    }
}
