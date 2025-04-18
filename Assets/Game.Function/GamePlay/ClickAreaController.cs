
using Core.Pool;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAreaController : MonoBehaviour
{
    public static ClickAreaController instance = null;

    [SerializeField] BallSetupController selectBall;

    [SerializeField] FactoryFXGenerator poolFX;

    [SerializeField] PoolFxPoint poolFxPoint;

    [SerializeField] ComboActive comboController;

    [SerializeField] ConfigPercentCreateBall configPercentCreateBall;

    [SerializeField] SoiCauBong viewNextBall;

    [SerializeField] Transform aimLine;

    [SerializeField] Transform posCreateBall;

    [SerializeField] Transform limitPosBallLeft, limitPosBallRight;

    [SerializeField] Sprite[] spriteBalls;

    [SerializeField] float valueScaleBall = 1;

    bool gameOver;

    int layerBall = 8;

    int[] listPoints = new int[] { 1, 3, 6, 10, 15, 25, 50, 90, 125, 175, 300 };

    int indexBallSelected;

    int currentIndexBall;

    Transform ballSelectEd;

    BallBase ball;

    Camera cam;

    Vector2 posAimLineDefault;

    Vector2 posLastBall;

    public bool dontClick = false;

    int maxIndexPercentCreate = 1;

    private void Awake()
    {
        instance = this;

        cam = Camera.main;

        posAimLineDefault = aimLine.position;

        InitPosCarb();
    }

    private void Start()
    {
        SelectBallStart();
    }

    void SelectBallStart()
    {
        ballSelectEd = SelectedBall(posCreateBall.position, 0);

        currentIndexBall = SelectIndexBall();

        UpdateImgNextBall(indexBallSelected);

        SetPosBallStart();

    }

    public void DelayMouseUp()
    {
        if (dontClick) 
            return;
        
        if (gameOver) 
            return;
        
        if (ballSelectEd == null) 
            return;

        posLastBall = ballSelectEd.position;

        FallBall();
    }

    Transform SelectedBall(Vector2 posCreate, int indexBall)
    {
        Transform _ball = null;

        _ball = selectBall.GetBall(posCreate, indexBall, valueScaleBall, layerBall);

        ball = _ball.GetComponent<Ball>();

        ball.SetData(indexBall);

        if (gameOver) _ball = null;

        return _ball;
    }

    int SelectIndexBall()
    {
        int index = 0;
        
        int difficult = 0;

        var probs = configPercentCreateBall.percentPhases[difficult].percentCreate;

        index = ChooseRandom(GetPercentCreateBall(probs));

        if (index < 0) index = 0;

        return index;
    }

    int[] GetPercentCreateBall(int[] array)
    {
        List<int> _list = new List<int>();

        for (int i = 0; i < maxIndexPercentCreate; i++)
        {
            _list.Add(array[i]);
        }

        return _list.ToArray();
    }

    public void SetPosBall(Vector2 newPos)
    {
        if (ballSelectEd == null) return;
        var pos = new Vector2(newPos.x, 4.1f);
        ball.SetPosBall(pos, limitPosBallLeft.position.x, limitPosBallRight.position.x);
    }

    void SetPosBallStart()
    {
        if (ballSelectEd == null) return;

        ball.SetPosBall(new Vector2(0, 4.1f), limitPosBallLeft.position.x, limitPosBallRight.position.x);
    }



    void FallBall()
    {
        if (ballSelectEd == null) return;

        ballSelectEd.GetComponent<BallBase>().ActiveRb(false);

        ballSelectEd = null;

        ResetPosAimLine();

        if (gameOver) return;

        StartCoroutine(AutoShowBall());
    }

    void ResetPosAimLine()
    {
        aimLine.position = posAimLineDefault;
    }

    void UpdateImgNextBall(int indexBall)
    {
        viewNextBall.ShowNewBall(spriteBalls[indexBall]);
    }

    void SelectNextBall()
    {
        currentIndexBall = indexBallSelected;

        indexBallSelected = SelectIndexBall();

        UpdateImgNextBall(indexBallSelected);
    }

    IEnumerator AutoShowBall()
    {
        yield return new WaitForSeconds(0.1f);

        SelectNextBall();

        yield return new WaitForSeconds(0.25f);

        ballSelectEd = SelectedBall(posLastBall, currentIndexBall);
    }

    public void MergeBall(Vector2 posCreate, int idBall)
    {
        if (gameOver) return;

        int newIdBall = idBall + 1;

        this.PostEvent(EventID.MergeSuccess, newIdBall);
        

        if (newIdBall > 10) 
            newIdBall = 10;

        maxIndexPercentCreate = maxIndexPercentCreate < (newIdBall + 1) && maxIndexPercentCreate < 6 ? (newIdBall + 1) : maxIndexPercentCreate;

        this.PostEvent(EventID.UpdatePoint, listPoints[idBall]);

        var newBall = selectBall.GetBall(posCreate, newIdBall, 0, layerBall);

        var _newBall = newBall.GetComponent<Ball>();


        _newBall.ZoomMerge(0);

        _newBall.SetData(newIdBall);

        _newBall.ActiveRb(true);

        poolFX.SpawnFx(posCreate, valueScaleBall);

        poolFxPoint.SpawnFx(idBall, posCreate, valueScaleBall);

        comboController.ShowCombo();

        VibrateMerge(idBall);

    }

    private int ChooseRandom(int[] probs)
    {
        float total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }

    public void SetStatusClick(bool status)
    {
        dontClick = !status;
    }

    public void SetStatusGameOver()
    {
        gameOver = true;

        if (ballSelectEd != null) ballSelectEd.gameObject.SetActive(false);

        aimLine.gameObject.SetActive(false);
    }

    public void ContinuePlay()
    {
        gameOver = false;

        aimLine.gameObject.SetActive(true);

        SelectBallStart();
    }

    void VibrateMerge(int idBall)
    {
        if (!UserData.statusVibrate) return;
    }

    void InitPosCarb()
    {
        posCreateBall.transform.position = new Vector2(posCreateBall.transform.position.x, 4.33f);
    }

    public void SetStatusActive(bool status)
    {
        gameObject.SetActive(status);

        if (status) { if (dontClick) dontClick = false; }
    }


    private void OnMouseDown()
    {
        DelayMouseDown();
    }

    private void OnMouseDrag()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (dontClick) return;

        if (gameOver) return;

        if (ballSelectEd == null) return;
    }

    private void DelayMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (dontClick) return;

        if (gameOver) return;

        if (ballSelectEd == null) return;

    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    int id = touch.fingerId;
                    if (EventSystem.current.IsPointerOverGameObject(id))
                    {
                        dontClick = true;
                        Debug.Log($"[clickUI] {dontClick}");
                    }
                    break;

                case TouchPhase.Moved:
                    break;

                case TouchPhase.Ended:
                    Invoke(nameof(ResetClick), 0.02f);
                    break;
            }
        }
    }

    private void ResetClick()
    {
        dontClick = false;
    }
}


