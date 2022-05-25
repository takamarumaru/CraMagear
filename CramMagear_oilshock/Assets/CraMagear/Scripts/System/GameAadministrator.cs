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
        Clear,
        GameOver,
    }
    GameState _state = GameState.FirstPhase;
    public GameState State { get => _state; set => _state = value; }


    [Tooltip("Phase1�̎���")]
    [SerializeField] private float _firstPhaseTime;
    private float _phaseCount;
    [Tooltip("�G�𐶐�����I�u�W�F�N�g")]
    [SerializeField] private Transform _enemyCreator;

    [Tooltip("�N���A���ɓǂݍ��ރV�[��")]
    [SerializeField] private string _clearScene;

    [Tooltip("�Q�[���I�[�o�[���ɓǂݍ��ރV�[��")]
    [SerializeField] private string _gameOverScene;

    private void Start()
    {
        EnterFirstPhase();
    }

    private void Update()
    {
        switch (_state)
        {
            case GameState.FirstPhase   :  FirstPhaseUpdate(); break;
            case GameState.SecondPhase  :  SecondPhaseUpdate(); break;
            case GameState.Clear        :  SceneManager.LoadScene(_clearScene); break;
            case GameState.GameOver     :  SceneManager.LoadScene(_gameOverScene); break;
            default:break;
        }
    }


    //1stPhase�̏���
    void EnterFirstPhase()
    {
        _enemyCreator.gameObject.SetActive(false);
        State = GameState.FirstPhase;
    }
    void FirstPhaseUpdate()
    {
        _phaseCount += Time.deltaTime;
        if(_phaseCount >= _firstPhaseTime)
        {
            EnterSecondPhase();
        }
    }


    //2ndPhase�̏���
    void EnterSecondPhase()
    {
        _enemyCreator.gameObject.SetActive(true);
        State = GameState.SecondPhase;
    }
    void SecondPhaseUpdate()
    {
        
    }
}
