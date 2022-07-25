using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArchitectureCreator : MonoBehaviour
{
    //���z����I�u�W�F�N�g�̏�񃊃X�g
    [System.Serializable]
    public class CreateArchitecture
    {
        public Transform guide;
        public Transform product;
        public IngredientsManager.IngredientsInfo needIngredients;
    }
    [SerializeField] private List<CreateArchitecture> _createArchitectureList = new();
    private int _selectIdx = 0;

    //�K�C�h�I�u�W�F�N�g�i�[�p
    private Transform _guide;

    //���z�ł���Collision���C���[
    [SerializeField] private LayerMask _collisionLayer;

    //���z�\�͈͐ݒ�
    [SerializeField] private Transform _center;
    [SerializeField] private float _architectureRange;

    //�����Ǘ�
    [SerializeField] private MembersAdministrator _membersAdministrator;
    //�f�ފǗ�
    [SerializeField] private IngredientsManager _ingredientsManager;


    public bool _enable { get; private set; }

    private void Awake()
    {
        Debug.Assert(_createArchitectureList.Count != 0, "ArchitectureCreator��_createArchitectureList����ł��B");
        Debug.Assert(_center != null, "ArchitectureCreator��Player���ݒ肳��Ă��܂���B");
        Debug.Assert(_membersAdministrator != null, "ArchitectureCreator��MembersAdministrator���ݒ肳��Ă��܂���B");
       
        //�ŏ��̃K�C�h�I�u�W�F�N�g�𐶐�
        _guide = Instantiate(_createArchitectureList[_selectIdx].guide);
        _guide.gameObject.SetActive(_enable);
    }

    public void Update()
    {
        //�f�o�b�O�p�̉��̏����A�{����Player��State����Switching()���Ăяo��
        if (PlayerInputManager.Instance.GamePlay_GetListSwitchingLeft())
        {
            Switching(-1);
        }
        if (PlayerInputManager.Instance.GamePlay_GetListSwitchingRight())
        {
            Switching(1);
        }
    }

    public void Switching(int difference)
    {
        if (_enable == false) return;

        int newIdx = _selectIdx + difference;
        //�͈͐���
        if (newIdx < 0) newIdx = _createArchitectureList.Count - 1;
        _selectIdx = newIdx % _createArchitectureList.Count;

        //�K�C�h�I�u�W�F�N�g�̍Đ���
        Vector3 guidePostion = _guide.transform.position;
        DestroyImmediate(_guide.gameObject);
        _guide = Instantiate(_createArchitectureList[_selectIdx].guide, guidePostion, Quaternion.identity);
    }

    //public void AddArchitecture(Transform guide,Transform architecture)
    public void AddArchitecture(CreateArchitecture architecture)
    {
        _createArchitectureList.Add(architecture);
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

        if(_ingredientsManager.UseIngredients(_createArchitectureList[_selectIdx].needIngredients) == false)
        {
            return false;
        }

        //���z���𐶐�
        Transform transform = Instantiate(_createArchitectureList[_selectIdx].product, _guide.position, _guide.rotation).transform;
        //member�����z���̏ꏊ�ɔh������
        if(_membersAdministrator.Dispatch(0,transform)==false)
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
