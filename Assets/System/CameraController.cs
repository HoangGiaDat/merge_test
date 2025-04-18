using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance = null;

    Camera cam;

    [SerializeField] AnimationCurve curve;

    [SerializeField] float duration = 0.5f;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);

            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);

        cam = GetComponent<Camera>();
    }

    void Start()
    {
        UpdateCameraSize();
    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
    }

    public void ShakeIt()
    {
        StartCoroutine(nameof(Shaking));
    }

    public void DelayShake()
    {
        Invoke(nameof(ShakeIt), 0.3f);
    }

#if UNITY_EDITOR
    private void Update()
    {
        UpdateCameraSize();
    }
#endif

    private void UpdateCameraSize()
    {
    }
}
