using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MembersAdministrator : MonoBehaviour
{
    [SerializeField] private int _createNum = 0;

    [SerializeField] private Transform _createMember;

    public class MemberInfo
    {
       public Transform _memberTransform;
       public GroupMemberInputProvider _input;
       public Transform _owner;
       public bool _isDispatch;
       public bool _isArrival;
    }

    private List<MemberInfo> _memberInfo = new ();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(_createMember != null, "MembersAdministratorにTransformが設定されていません。");

        for (int i = 0; i < _createNum; i++) 
        {
            //メンバーを生成
            GameObject newMember = Instantiate(_createMember.gameObject, transform);

            //情報を取得
            MemberInfo info = new MemberInfo();
            info._memberTransform = newMember.transform;
            info._input = info._memberTransform.GetComponentInChildren<GroupMemberInputProvider>();
            info._owner = newMember.GetComponentInChildren<GroupMemberInputProvider>().TargetTransform;
            info._isDispatch = false;
            info._isArrival = false;

            //リストに追加
            _memberInfo.Add(info);
        }
    }

    private void Update()
    {
        for (int memberIdx = 0; memberIdx < _memberInfo.Count; memberIdx++)
        {
            if (_memberInfo[memberIdx]._input.TargetTransform == null)
            {
                _memberInfo[memberIdx]._input.TargetTransform = _memberInfo[memberIdx]._owner;
                _memberInfo[memberIdx]._isDispatch = false;
            }
            else
            {
                Transform memberTransform = _memberInfo[memberIdx]._memberTransform;
                Transform targetTransform = _memberInfo[memberIdx]._input.TargetTransform;
                var nav = memberTransform.GetComponent<NavMeshAgent>();
                _memberInfo[memberIdx]._isArrival = ((memberTransform.position - targetTransform.position).magnitude <= 5.0f);
                //Debug.Log(memberIdx.ToString()+":" + _memberInfo[memberIdx]._isArrival);
            }
        }
    }

    public bool Dispatch(int dispatchNum,Transform dispatchTransform)
    {
        var architectureGuideManager = dispatchTransform.GetComponent<ArchitectureWhenMembersCome>();
        int createCount = 0;
        for (int memberIdx = 0 ; createCount < dispatchNum; memberIdx++)
        {
            if (memberIdx >= _memberInfo.Count) break;
            if (_memberInfo[memberIdx]._isDispatch == false)
            {
                _memberInfo[memberIdx]._input.TargetTransform = dispatchTransform;

                _memberInfo[memberIdx]._isDispatch = true;

                architectureGuideManager.SetMember(_memberInfo[memberIdx]);
                createCount++;
            }
        }
        if(createCount < dispatchNum)
        {
            return false;
        }
        return true;
    }
}
