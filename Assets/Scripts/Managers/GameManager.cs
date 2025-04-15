using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public static SoundManager Sound { get { return Instance._sound; } }
    //public static MapManager Map { get { return Instance._map; } }
    public static DataManager Data { get { return Instance._data; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static TruckManager Truck { get { return Instance._truck; } }
    public static CrewManager CrewManager { get { return Instance._crew; } }
    public static InputManager Input { get { return Instance._input; } }
    public static WeaponManager WeaponManager { get { return Instance._weapon; } }
    public static CombatManager Combat { get { return Instance._combat; } }

    private SoundManager _sound = new SoundManager();
    //private MapManager _map = new MapManager();
    private DataManager _data = new DataManager();
    private UIManager _ui = new UIManager();
    private TruckManager _truck = new TruckManager();
    private CrewManager _crew = new CrewManager();
    private InputManager _input = new InputManager();
    private WeaponManager _weapon = new WeaponManager();
    private CombatManager _combat = new CombatManager();

    private State _currentState = State.Idle;
    public State CurrentState { get { return _currentState; } set { _currentState = value; } }

    bool _isPaused = false;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            Init();
        }
    }

    private void Init()
    {
        // 액션 순서 관리를 위한 Init 순서 입니다. 따라서 액션 추가할때 Init 순서 변경해주세요.
        Input.Init();
        Sound.Init();
        //Map.Init();
        Data.Init();
        Truck.Init();
        CrewManager.Init();
        UI.Init();
        WeaponManager.Init();
        Combat.Init();

    }

    private void Start()
    {
        Input.pauseGame += PauseGame;
        // 테스트 용 스폰
        CrewManager.Spawn(CrewManager.ShowCrew());
        //CrewManager.Spawn(CrewManager.ShowCrew());
        WeaponManager.SetWeapon(WeaponManager.GetRandomWeapon(), 0);
        WeaponManager.SetWeapon(WeaponManager.GetRandomWeapon(), 1);

        NodeEvent nodeEvent = new GameObject("HireEvent").AddComponent<HireEvent>();
        nodeEvent.ShowEventCanvas();
    }

    public void PauseGame()
    {
        UI.UI_Canvas.PauseGame();
        if(_isPaused)
        {
            Time.timeScale = 1f;
            _isPaused = false;
        } else
        {
            Time.timeScale = 0f;
            _isPaused = true;
        }
    }
    public void ChangeState(State currentState)
    {
        _currentState = currentState;
    }

    /// <summary>
    /// 게임 오버 시 호출 
    /// 게임 종료 신으로 이동
    /// 0: 게임 클리어
    /// 1: 연료부족 게임 오버
    /// 2: 트럭 파괴 게임 오버
    /// </summary>
    /// <param name="code"></param>
    public void GameOver(int code)
    {
        switch (code)
        {
            case 0:
                SceneManager.LoadScene("GameClearScene");
                break;
            case 1:
                SceneManager.LoadScene("FuelLossGameOverScene");
                break;
            case 2:
                SceneManager.LoadScene("TruckDestroyGameOverScene");
                break;
            default:
                break;
        }
    }
}
