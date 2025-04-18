using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayClassicController : MonoBehaviour
{
    public static GamePlayClassicController instance = null;

    [SerializeField] ClickAreaController clickArea;

    [SerializeField] PoolingObjectManager poolManager;

    [SerializeField] Text textPoint;

    [SerializeField] Text textHighScore;

    [SerializeField] GameObject popupGameOver;

    [SerializeField] GameSeekerOver gameOverCollider;

    [HideInInspector] public int point = 0;

    [HideInInspector] public bool isLose;

    Transform trasformPoint;

    bool enableZoomPoint = true;

    int highScore = 0;

    private bool isPaused = false;
    

    private void Awake()
    {
        instance = this;

        trasformPoint = textPoint.transform;

        RegisterListenerEvent();
    }

    private void OnEnable()
    {
        Time.timeScale = 1;
        ClickAreaController.instance.SetStatusClick(true);
    }
    private void Start()
    {
        UserData.countPlayGame++;
        textHighScore.text = string.Empty;
    }

    private void OnApplicationPause(bool pause)
    {
        isPaused = pause;

        if (isPaused)
        {
            PoolingObjectManager.instance.SaveGameState();
        }
        else
        {
            HandClickController.Instance.popUpisActive = false;
            Time.timeScale = 1;
            ClickAreaController.instance.SetStatusClick(true);
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            PoolingObjectManager.instance.SaveGameState();
        }
        else
        {
            HandClickController.Instance.popUpisActive = false;
            Time.timeScale = 1;
            ClickAreaController.instance.SetStatusClick(true);
        }
    }

    void OnApplicationQuit()
    {
        PoolingObjectManager.instance.SaveGameState();
    }

    void RegisterListenerEvent()
    {
        this.RegisterListener(EventID.UpdatePoint, (sender,param) => { UpdatePoint((int) param); });
    }
    
    public void UpdatePoint(int _point)
    {
        point += _point;

        textPoint.text = point.ToString();
        ZoomPoint();

        if (point > highScore)
        {
            textHighScore.text = point.ToString();
        }
    }

    void ZoomPoint()
    {
        if (!enableZoomPoint) return;

        enableZoomPoint = false;

        trasformPoint.DOScale(Vector2.one * 1.25f, 0.15f).SetEase(Ease.InSine).onComplete = () => { UnZoomPoint(); };
    }

    void UnZoomPoint()
    {
        trasformPoint.DOScale(Vector2.one, 0.15f).SetEase(Ease.OutSine).onComplete = () => { enableZoomPoint = true; };
    }

    public void GameOver()
    {
        if (isLose) return;

        isLose = true;

        clickArea.SetStatusGameOver();
        
        if(point > UserData.curentScore) 
            UserData.curentScore = point;

        Invoke(nameof(DelayShakeBall), 0.5f);
        Invoke(nameof(ShowPopupGameOver), 3f);
    }

    void DelayShakeBall()
    {
        poolManager.ShakeAllBall();
    }

    void ShowPopupGameOver()
    {
        popupGameOver.SetActive(true);
    }

    public void ActionReplay()
    {
        SceneManager.LoadScene(SceneTypes.modeBase);
    }

    public void ContinuePlay()
    {
        gameOverCollider.ResetStatusLose();

        gameOverCollider.gameObject.SetActive(false);
    }
    
    public void OnTapHome()
    {
        SceneManager.LoadScene(SceneTypes.home);

    }
}
