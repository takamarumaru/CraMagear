using UnityEngine;

namespace GameStateMachine
{
    //=====================================
    /// <summary>
    /// ステートマシンノード基本クラス
    /// </summary>
    //=====================================
    [System.Serializable]
    public class StateNodeBase
    {
        //このステートに入った時、1度だけ実行
        public virtual void OnEnter() { }

        //このステートから出る時、１度だけ実行
        public virtual void OnExit() { }

        //毎フレーム実行
        public virtual void OnUpdate() { }

        //一定時間ごとに実行
        public virtual void OnFixedUpdate() { }

        //管理元のマネージャーへの参照
        StateMachineManager _stateMgr;

        //アニメーターへの参照
        Animator _animator;

        public Animator Animator => _animator;

        //ゲッター
        public StateMachineManager StateMgr => _stateMgr;

        public void Initialize(Animator animator, StateMachineManager mgr)
        {
            _animator = animator;
            _stateMgr = mgr;
        }

        //仮
        [SerializeField] int _date;
    }

    /// <summary>
    /// ステートマシーン管理クラス
    /// </summary>
    public class StateMachineManager : MonoBehaviour
    {
        [SerializeField] CharacterBrain _charaBrain;

        public CharacterBrain CharaBrain => _charaBrain;

        //現在のステート
        StateNodeBase _nowState;

        //ステート変更
        public void ChangeState(StateNodeBase state)
        {
            //?:nullチェックしてくれる自作のみ（MonoBehaverなどでは使用不可）
            _nowState?.OnExit();

            _nowState = state;


            _nowState?.OnEnter();

        }

        //更新処理
        private void Update()
        {
            if (_nowState == null) { return; }

            _nowState.OnUpdate();

            _nowState.OnFixedUpdate();
        }
    }
}
