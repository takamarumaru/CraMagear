using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMagicCircle : MonoBehaviour
{
    [SerializeField, Header("���@�w�̃v���n�u")]
    Transform _magicCircle;

    // ���@�w�̈���
    [SerializeField]
    List<Layer> _layers;

    [SerializeField, Header("�I�[���̃v���n�u")]
    Transform _aura;

    [SerializeField, Header("�����̃e���v���[�g")]
    GameObject _character;

    [SerializeField, Header("�~�`�̕�����")]
    List<MagicCircleString> _magicCircleStrings;

    [SerializeField, Header("���@�w�H�����܂Ȃ����߂̐��l")]
    float _adjustment = 1.0f;

    void Awake()
    {
        // ���@�w���\������S�Ă̐e�I�u�W�F�N�g
        //var parentObj = new GameObject("MagicCircle");
        var parentObj = new GameObject("MagicCircle");

        //���������ꏊ�ɏo�������邽��
        var changeScaleRotatePosition = parentObj.AddComponent<ChangeScaleRotate>();

        //���@�w�ݒu�̂��ߔ�����
        changeScaleRotatePosition._popPosition = new Vector3(transform.position.x, transform.position.y + _adjustment, transform.position.z);

        //parentObj.transform.position = new Vector3(0, 1.0f, 0);

        MagicCircle(ref parentObj);

        Aura(ref parentObj);

        Text(ref parentObj);

        //�G�t�F�N�g���쐬������͎��g���폜����
        Destroy(gameObject);
    }

    void Update()
    {
        //  Create();
    }

    public void Create()
    {

    }

    void MagicCircle(ref GameObject magicCircleParentObj)
    {
        // ���@�w�̐e�I�u�W�F�N�g
        var parentObj = new GameObject("MagicCircle");
        parentObj.transform.parent = magicCircleParentObj.transform;

        // �e�̒��Ɏq�i���C���[�j�𐶐�
        for (int i = 0; i < _layers.Count; i++)
        {
            // �����������鎞�͐e�Ǝq�̊Ԃ�GameObject������āA���̒��Ɏq�����
            GameObject middleObj = new GameObject("MagicCircle " + i);
            middleObj.transform.parent = parentObj.transform;

            for (uint instanceNum = 0; instanceNum < _layers[i].InstanceNum; instanceNum++)
            {
                GameObject childObj = Instantiate(_magicCircle.gameObject);
                VFX_MagicCircle childMagicCircle = childObj.GetComponent<VFX_MagicCircle>();
                if (_layers[i].IsCopy)
                {
                    if (i == 0)
                    {
                        Debug.LogError("Element 0 �� IsCopy �Ƀ`�F�b�N�͓�����܂���I");
                    }
                    _layers[i].Initialize(_layers[i - 1]);
                }
                childMagicCircle.Initialize(childMagicCircle, _layers[i]);
                childMagicCircle.transform.parent = _layers[i].InstanceNum == 1 ? parentObj.transform : middleObj.transform;

                // ���O��ύX
                childMagicCircle.name += " " + i;
                if (_layers[i].InstanceNum > 1) childMagicCircle.name += "_" + instanceNum;
            }

            // 1������������ꍇ�͍폜����
            if (_layers[i].InstanceNum == 1) Destroy(middleObj);
        }
    }

    void Aura(ref GameObject magicCircleParentObj)
    {
        if (_aura == null) return;

        // �n�ʂ���N���オ��I�[���𐶐�
        GameObject auraObj = Instantiate(_aura.gameObject, magicCircleParentObj.transform);
        VFX_Aura aura = auraObj.GetComponent<VFX_Aura>();
        aura.VFXCommon.SetFloat(_layers[0].Scale / 20.0f, "Scale");
    }

    void Text(ref GameObject magicCircleParentObj)
    {
        // �S�Ă̕�������i�[����I�u�W�F�N�g
        GameObject parentObj = new GameObject("Text");
        parentObj.transform.parent = magicCircleParentObj.transform;

        for (int strings = 0; strings < _magicCircleStrings.Count; strings++)
        {
            MagicCircleString MCS = _magicCircleStrings[strings];
            float angleRange = MCS.AngleRange;

            // ��������i�[����I�u�W�F�N�g
            GameObject childObj = new GameObject(MCS.Str);
            childObj.transform.parent = parentObj.transform;

            for (int instanceNum = 0; instanceNum < MCS.InstanceNum; instanceNum++)
            {
                for (int chara = 0; chara < MCS.Str.Length; chara++)
                {
                    char c = MCS.Str[chara];

                    // �������i�[����I�u�W�F�N�g
                    GameObject charObj = Instantiate(_character);
                    charObj.transform.parent = childObj.transform;
                    charObj.transform.position = Vector3.forward * MCS.Radius;
                    charObj.transform.Rotate(new Vector3(90, 0, 0));

                    // �������^�ɕ��ׂ�
                    float angle = chara * 2 * angleRange / (MCS.Str.Length - 1) - angleRange;
                    angle += MCS.OffsetAngle;
                    angle += 360.0f / MCS.InstanceNum * instanceNum;
                    charObj.transform.RotateAround(magicCircleParentObj.transform.position, magicCircleParentObj.transform.up, angle);

                    charObj.SetActive(true);
                    TextMesh textMesh = charObj.GetComponent<TextMesh>();
                    textMesh.text = c.ToString();
                    textMesh.anchor = MCS.Ancor;
                    textMesh.color = MCS.Color;

                    var MCSU = charObj.AddComponent<MagicCircleStringUpdate>();
                    MCSU.Initialize(magicCircleParentObj.transform, MCS.OffsetAngleSpeed);
                }
            }
        }
    }
}
