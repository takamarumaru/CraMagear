using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameAadministrator : MonoBehaviour
{
    static public GameAadministrator Instance { get; private set; }

    public enum GameState 
    { 
        FirstPhase,
        SecondPhase,
        ThirdPhase,
        Clear,
        GameOver,
    }
    GameState _state = GameState.FirstPhase;
    public GameState State { get => _state; set => _state = value; }


    [Tooltip("Phase1の時間")]
    [SerializeField] private float _firstPhaseTime;
    [Tooltip("Phase2の時間")]
    [SerializeField] private float _secondPhaseTime;

    public float PhaseCount { get; private set; }
    [Tooltip("敵を生成するオブジェクト")]
    [SerializeField] private Transform _enemyCreator;
    private List<Transform> _enemyCreatorList = new();
    private int _enableIdx = 0;
    public Transform EnableEnemyCreator()
    {
        if(_enemyCreatorList.Count <= _enableIdx)return null;
        return _enemyCreatorList [_enableIdx]; 
    }

    [Tooltip("クリア時に読み込むシーン")]
    [SerializeField] private string _clearScene;

    [Tooltip("ゲームオーバー時に読み込むシーン")]
    [SerializeField] private string _gameOverScene;

    private void Start()
    {
        Debug.Assert(_enemyCreator != null, "GameAadministratorに敵を生成するオブジェクトが設定されていません。");

        EnterFirstPhase();
    }

    private void Update()
    {
        switch (_state)
        {
            case GameState.FirstPhase   :  FirstPhaseUpdate(); break;
            case GameState.SecondPhase  :  SecondPhaseUpdate(); break;
            case GameState.ThirdPhase   :  ThirdPhaseUpdate(); break;
            case GameState.Clear        :  SceneManager.LoadScene(_clearScene); break;
            case GameState.GameOver     :  SceneManager.LoadScene(_gameOverScene); break;
            default:break;
        }
    }


    //1stPhaseの処理--------------------------------------------------------------------
    void EnterFirstPhase()
    {
        //カウンターに時間をセット
        PhaseCount = _firstPhaseTime;

        //指定オブジェクトの子供リストを作る
        for (int childIdx = 0; childIdx < _enemyCreator.childCount; childIdx++)
        {
            Transform child = _enemyCreator.GetChild(childIdx);
            child.gameObject.SetActive(false);
            _enemyCreatorList.Add(child);
        }

        //有効になる敵湧き地点を決定
        _enableIdx = Random.Range(0, _enemyCreatorList.Count);

        State = GameState.FirstPhase;
    }
    void FirstPhaseUpdate()
    {
        PhaseCount -= Time.deltaTime;
        if(PhaseCount <= 0.0f)
        {
            PhaseCount = 0.0f;
            EnterSecondPhase();
        }
    }


    //2ndPhaseの処理--------------------------------------------------------------------
    void EnterSecondPhase()
    {
        //カウンターに時間をセット
        PhaseCount = _secondPhaseTime;

        _enemyCreatorList[_enableIdx].gameObject.SetActive(true);

        State = GameState.SecondPhase;
    }
    void SecondPhaseUpdate()
    {
        PhaseCount -= Time.deltaTime;
        if (PhaseCount <= 0.0f)
        {
            PhaseCount = 0.0f;
            EnterThirdPhase();
        }
    }

    //3rdPhaseの処理--------------------------------------------------------------------
    void EnterThirdPhase()
    {
        _enemyCreatorList[_enableIdx].gameObject.SetActive(true);

        State = GameState.ThirdPhase;
    }
    void ThirdPhaseUpdate()
    {

    }
}
