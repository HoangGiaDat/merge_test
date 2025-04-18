using CW.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandClickController : SingletonMono<HandClickController>
{
    public GameObject handSpawn;
    public ClickAreaController clickAreaController;
    public bool popUpisActive;
    private bool _isTouch;
    private bool _isLose;

    public virtual void OnEnable()
    {
        CwInputManager.OnFingerDown += HandleFingerDown;
        CwInputManager.OnFingerUp += HandleFingerUp;
        CwInputManager.OnFingerUpdate += HandleFingerUpdate;
        _isTouch = false;
    }

    public virtual void OnDisable()
    {
        CwInputManager.OnFingerDown -= HandleFingerDown;
        CwInputManager.OnFingerUp -= HandleFingerUp;
        CwInputManager.OnFingerUpdate -= HandleFingerUpdate;

    }

    public virtual void OnDestroy()
    {
        CwInputManager.OnFingerDown -= HandleFingerDown;
        CwInputManager.OnFingerUp -= HandleFingerUp;
        CwInputManager.OnFingerUpdate -= HandleFingerUpdate;
    }

    public virtual void HandleFingerDown(CwInputManager.Finger finger)
    {
        if (!popUpisActive)
        {
            handSpawn.gameObject.SetActive(true);
            
        }
    }

    public virtual void HandleFingerUp(CwInputManager.Finger finger)
    {
        if (!popUpisActive)
        {
            if (clickAreaController.isActiveAndEnabled)
            {
                clickAreaController.DelayMouseUp();
            }
        }
        handSpawn.gameObject.SetActive(false);
    }

    public virtual void HandleFingerUpdate(CwInputManager.Finger finger)
    {
        if (_isTouch && Input.touchCount < 2 && !_isLose && !popUpisActive)
        {
            
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3
                (Input.mousePosition.x,
                (Input.mousePosition.y),
                (transform.position.z - Camera.main.transform.position.z)));
            handSpawn.transform.localPosition = new Vector3(pos.x, handSpawn.transform.localPosition.y, handSpawn.transform.localPosition.z);
            Vector3 a = new Vector3(pos.x, 4.1f, handSpawn.transform.localPosition.z);
            clickAreaController.SetPosBall(a);
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse was clicked over a UI element
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Clicked on the UI");
            }
            else
            {
                _isTouch = true;
            }
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Check if finger is over a UI element
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                Debug.Log("Clicked on the UI");
                return;
            }
            Debug.Log("Clicked on the GameObject");
            _isTouch = true;
        }
    }
}
