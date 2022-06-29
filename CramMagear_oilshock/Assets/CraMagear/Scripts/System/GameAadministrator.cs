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


    [Tooltip("Phase1�̎���")]
    [SerializeField] private float _firstPhaseTime;
    [Tooltip("Phase2�̎���")]
    [SerializeField] private float _secondPhaseTime;

    public float PhaseCount { get; private set; }
    [Tooltip("�G�𐶐�����I�u�W�F�N�g")]
    [SerializeField] private Transform _enemyCreator;
    private List<Transform> _enemyCreatorList = new();
    private int _enableIdx = 0;
    public Transform EnableEnemyCreator()
    {
        if(_enemyCreatorList.Count <= _enableIdx)return null;
        return _enemyCreatorList [_enableIdx]; 
    }

    [Tooltip("�N���A���ɓǂݍ��ރV�[��")]
    [SerializeField] private string _clearScene;

    [Tooltip("�Q�[���I�[�o�[���ɓǂݍ��ރV�[��")]
    [SerializeField] private string _gameOverScene;

    private void Start()
    {
        Debug.Assert(_enemyCreator != null, "GameAadministrator�ɓG�𐶐�����I�u�W�F�N�g���ݒ肳��Ă��܂���B");

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


    //1stPhase�̏���--------------------------------------------------------------------
    void EnterFirstPhase()
    {
        //�J�E���^�[�Ɏ��Ԃ��Z�b�g
        PhaseCount = _firstPhaseTime;

        //�w��I�u�W�F�N�g�̎q�����X�g�����
        for (int childIdx = 0; childIdx < _enemyCreator.childCount; childIdx++)
        {
            Transform child = _enemyCreator.GetChild(childIdx);
            child.gameObject.SetActive(false);
            _enemyCreatorList.Add(child);
        }

        //�L���ɂȂ�G�N���n�_������
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


    //2ndPhase�̏���--------------------------------------------------------------------
    void EnterSecondPhase()
    {
        //�J�E���^�[�Ɏ��Ԃ��Z�b�g
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

    //3rdPhase�̏���--------------------------------------------------------------------
    void EnterThirdPhase()
    {
        _enemyCreatorList[_enableIdx].gameObject.SetActive(true);

        State = GameState.ThirdPhase;
    }
    void ThirdPhaseUpdate()
    {

    }
}
