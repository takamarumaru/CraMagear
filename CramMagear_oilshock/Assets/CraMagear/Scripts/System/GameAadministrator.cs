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
        FinalPhase,
        Clear,
        GameOver,
    }
    GameState _state = GameState.FirstPhase;
    public GameState State { get => _state; set => _state = value; }


    [Tooltip("Phase1の時間")]
    [SerializeField] private float _firstPhaseTime;
    [Tooltip("Phase2の時間")]
    [SerializeField] private float _secondPhaseTime;

    [Tooltip("Phase2の時間")]
    [SerializeField] private int _randomSpawnInterval;
    private float _randomspawnCount;

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

        //指定オブジェクトの子供リストを作る
        for (int childIdx = 0; childIdx < _enemyCreator.childCount; childIdx++)
        {
            Transform child = _enemyCreator.GetChild(childIdx);
            child.gameObject.SetActive(false);
            _enemyCreatorList.Add(child);
        }

        EnterFirstPhase();
    }

    private void Update()
    {
        switch (_state)
        {
            case GameState.FirstPhase   :  FirstPhaseUpdate(); break;
            case GameState.SecondPhase  :  SecondPhaseUpdate(); break;
            case GameState.FinalPhase   :  FinalPhaseUpdate(); break;
            case GameState.Clear        :  SceneManager.LoadScene(_clearScene); break;
            case GameState.GameOver     :  SceneManager.LoadScene(_gameOverScene); break;
            default:break;
        }
    }


    //敵出現の処理----------------------------------------------------------------------

    void EnemyRandomSpawn()
    {
        //有効になる敵湧き地点を決定
        _enableIdx = Random.Range(0, _enemyCreatorList.Count);
        //敵生成オブジェクトを無効に
        EnemySpawnAllDisable();
        //有効になった敵生成オブジェクトを有効化
        EnableEnemyCreator().gameObject.SetActive(true);
    }

    void EnemySpawnAllEnable()
    {
        foreach(Transform enemyCreator in _enemyCreatorList)
        {
            enemyCreator.gameObject.SetActive(true);
        }
    }
    void EnemySpawnAllDisable()
    {
        foreach (Transform enemyCreator in _enemyCreatorList)
        {
            //enemyCreator.gameObject.SetActive(false);
            enemyCreator.GetComponent<VFX_EnemySpawn>().CangeState(VFX_EnemySpawn.GateState.Disappear);            
        }
    }

    //1stPhaseの処理--------------------------------------------------------------------
    void EnterFirstPhase()
    {
        //カウンターに時間をセット
        PhaseCount = _firstPhaseTime;

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
        State = GameState.SecondPhase;
        //カウンターに時間をセット
        PhaseCount = _secondPhaseTime;

        EnemyRandomSpawn();

    }
    void SecondPhaseUpdate()
    {

        //n秒ごとにランダムで敵の出現位置を変更
        if (_randomspawnCount >= _randomSpawnInterval)
        {
            EnemyRandomSpawn();
            _randomspawnCount = 0;
        }
        _randomspawnCount += Time.deltaTime;

        PhaseCount -= Time.deltaTime;
        if (PhaseCount <= 0.0f)
        {
            PhaseCount = 0.0f;
            EnterFinalPhase();
        }
    }

    //3rdPhaseの処理--------------------------------------------------------------------
    void EnterFinalPhase()
    {
        State = GameState.FinalPhase;
        //全ての場所から敵を出現
        EnemySpawnAllEnable();
    }
    void FinalPhaseUpdate()
    {

    }
}
