using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchitectureCreator : MonoBehaviour
{
    [SerializeField] private Transform _architectureGuide;
    [SerializeField] private Transform _architecturePrefab;

    [SerializeField] private LayerMask _collisionLayer;

    [SerializeField] private Transform _center;
    [SerializeField] private float _architectureRange;

    [SerializeField] private MembersAdministrator _membersAdministrator;


    private Transform _guide;

    public bool _enable { get; private set; }

    private void Awake()
    {
        Debug.Assert(_architectureGuide != null, "ArchitectureCreator��Object���ݒ肳��Ă��܂���B");
        Debug.Assert(_architecturePrefab != null, "ArchitectureCreator��PrefabObject���ݒ肳��Ă��܂���B");
        Debug.Assert(_center != null, "ArchitectureCreator��Player���ݒ肳��Ă��܂���B");
        Debug.Assert(_membersAdministrator != null, "ArchitectureCreator��MembersAdministrator���ݒ肳��Ă��܂���B");
       
        _guide = Instantiate(_architectureGuide);
        _guide.gameObject.SetActive(_enable);
    }

    public void ShowGuide()
    {
        if (_enable == false) return;

        //��ʂ̒��S�Ń��C����
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, Mathf.Infinity,_collisionLayer))
        {
            _guide.position = hit.point;
            
        }

        //�����������W���͈͓��Ȃ�K�C�h�I�u�W�F�N�g��\��
        if((hit.point-_center.position).magnitude > _architectureRange)
        {
            _guide.gameObject.SetActive(false);
        }
        else
        {
            _guide.gameObject.SetActive(true);
        }
    }

    public bool Create()
    {
        //�K�C�h�I�u�W�F�N�g����A�N�e�B�u�Ȃ���s���Ȃ�
        if (_guide.gameObject.activeSelf == false) return false;

        //���z���𐶐�
        Transform transform = Instantiate(_architecturePrefab, _guide.position, _guide.rotation).transform;
        //member�����z���̏ꏊ�ɔh������
        if(_membersAdministrator.Dispatch(3,transform)==false)
        {
            Destroy(transform.gameObject);
        }

        return true;
    }

    public void EnableToggle()
    {
        _enable = !_enable;
        _guide.gameObject.SetActive(_enable);
    }
}
