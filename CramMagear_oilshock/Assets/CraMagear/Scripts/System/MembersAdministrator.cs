using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MembersAdministrator : MonoBehaviour
{
    [SerializeField] private int _createNum = 0;

    [SerializeField] private Transform _createMember;

    class MemberInfo
    {
       public Transform _memberTransform;
       public GroupMemberInputProvider _input;
       public Transform _owner;
       public bool _isDispatch;
    }

    private List<MemberInfo> _memberInfo = new ();

    // Start is called before the first frame update
    void Start()
    {
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
        }
    }

    public void Dispatch(int dispatchNum,Transform dispatchTransform)
    {
        int createCount = 0;
        for (int memberIdx = 0 ; createCount < dispatchNum; memberIdx++)
        {
            if (memberIdx >= _memberInfo.Count) break;
            if (_memberInfo[memberIdx]._isDispatch == false)
            {
                _memberInfo[memberIdx]._input.TargetTransform = dispatchTransform;

                _memberInfo[memberIdx]._isDispatch = true;
                createCount++;
            }
        }
    }
}
