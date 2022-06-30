using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchitectureWhenMembersCome : MonoBehaviour
{
    private List<MembersAdministrator.MemberInfo> _memberInfo = new ();
    [SerializeField] Transform _architecturePrefab;

    public void SetMember(MembersAdministrator.MemberInfo info)
    {
        _memberInfo.Add(info);
    }

    // Update is called once per frame
    void Update()
    {
        if (_memberInfo.Count == 0)
        {
            Create();
            return;
        }

        bool isArrival = true;
        foreach (MembersAdministrator.MemberInfo info in _memberInfo)
        {
            isArrival &= info._isArrival;
        }
        if(isArrival)
        {
            Create();
        }
    }

    void Create()
    {
        Transform architectureTransform = Instantiate(_architecturePrefab, transform.position, transform.rotation);
        for (int memberIdx = 0; memberIdx < _memberInfo.Count; memberIdx++)
        {
            _memberInfo[memberIdx]._input.TargetTransform = architectureTransform;
        }
        Destroy(this.gameObject);
    }
}
