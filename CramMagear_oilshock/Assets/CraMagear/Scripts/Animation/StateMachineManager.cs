using UnityEngine;

namespace GameStateMachine
{
    //=====================================
    /// <summary>
    /// �X�e�[�g�}�V���m�[�h��{�N���X
    /// </summary>
    //=====================================
    [System.Serializable]
    public class StateNodeBase
    {
        //���̃X�e�[�g�ɓ��������A1�x�������s
        public virtual void OnEnter() { }

        //���̃X�e�[�g����o�鎞�A�P�x�������s
        public virtual void OnExit() { }

        //���t���[�����s
        public virtual void OnUpdate() { }

        //��莞�Ԃ��ƂɎ��s
        public virtual void OnFixedUpdate() { }

        //�Ǘ����̃}�l�[�W���[�ւ̎Q��
        StateMachineManager _stateMgr;

        //�A�j���[�^�[�ւ̎Q��
        Animator _animator;

        public Animator Animator => _animator;

        //�Q�b�^�[
        public StateMachineManager StateMgr => _stateMgr;

        public void Initialize(Animator animator, StateMachineManager mgr)
        {
            _animator = animator;
            _stateMgr = mgr;
        }

        //��
        [SerializeField] int _date;
    }

    /// <summary>
    /// �X�e�[�g�}�V�[���Ǘ��N���X
    /// </summary>
    public class StateMachineManager : MonoBehaviour
    {
        [SerializeField] CharacterBrain _charaBrain;

        public CharacterBrain CharaBrain => _charaBrain;

        //���݂̃X�e�[�g
        StateNodeBase _nowState;

        //�X�e�[�g�ύX
        public void ChangeState(StateNodeBase state)
        {
            //?:null�`�F�b�N���Ă���鎩��̂݁iMonoBehaver�Ȃǂł͎g�p�s�j
            _nowState?.OnExit();

            _nowState = state;


            _nowState?.OnEnter();

        }

        //�X�V����
        private void Update()
        {
            if (_nowState == null) { return; }

            _nowState.OnUpdate();

            _nowState.OnFixedUpdate();
        }
    }
}
