using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMB_Test : StateMachineBehaviour
{

    [SerializeReference,SubclassSelector] GameStateMachine.StateNodeBase _state;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //初回の場合は、初期設定をする
       if(_state.StateMgr==null)
        {
            var mgr = animator.GetComponent<GameStateMachine.StateMachineManager>();
            _state.Initialize(animator, mgr);
        }

        //StateMachineManagerのNowStateを切り替える
        _state.StateMgr.ChangeState(_state);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
