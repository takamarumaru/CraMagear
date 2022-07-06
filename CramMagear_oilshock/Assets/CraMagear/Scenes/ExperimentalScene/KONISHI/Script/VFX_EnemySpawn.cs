using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VFX_Common))]

public class VFX_EnemySpawn : MonoBehaviour
{
    private VFX_Common _vfxCommon;

    [SerializeField, Header("�ő�ɂȂ�܂ł̎���")]
    float _sphereSizeGrowTime = 1.0f;

    [SerializeField, Header("�d�C�G�t�F�N�g�̑傫��")]
    float _electricitySizeOffset = 1.0f;

    [SerializeField, Header("�f�B�]���u�ŏ�����܂ł̎���")]
    float _dissolveTakeTime = 1.0f;

    bool _isCloseGate = false;

    // Sphere
    float _sphereSize;              // Sphere�̍��̑傫��
    float _maxSphereSize;           // Sphere�̍ő�̑傫��
    float _growSpeed;               // Sphere���傫���Ȃ鑬��

    // �d�C�G�t�F�N�g
    float _electricitySize;         // �d�C�̃G�t�F�N�g�̑傫��
    float _electricityTakeTime = 1; // ���Ŏ��Ɋ|���鎞��
    float _electricityProgress;

    // �f�B�]���u
    float _dissolveThreshold = 1;   // �f�B�]���u�̓x����
    float _dissolveSpeed;           // �f�B�]���u�̑���

    // �������C�g
    Color _rimLightColor;
    float _rimLightTakeTime = 2;    // �������C�g�Ɋ|���鎞��
    float _rimLightProgress = 0;

    public enum GateState
    {
        Appear,
        Open,
        Disappear
    }
    private GateState _nowState = new GateState();

    public void CangeState(GateState gateState)
    {
        _nowState = gateState;
    }

    void Awake()
    {
        _vfxCommon = GetComponent<VFX_Common>();

        _nowState = GateState.Appear;

        _maxSphereSize = _vfxCommon.GetFloat("MaxSphereSize");

        _growSpeed = _maxSphereSize / _sphereSizeGrowTime;
        _dissolveSpeed = 1 / _dissolveTakeTime;

        _electricityTakeTime = _dissolveTakeTime;

        _rimLightColor = _vfxCommon.GetColor("RimLightColor");

        // �������C�g�͍ŏ��͓���
        Color color = new Color();
        _vfxCommon.SetColor(color, "RimLightColor");
    }

    private void OnEnable()
    {
        _nowState = GateState.Appear;
    }

    void Update()
    {
        Appear();

        Open();

        Disappear();

        _vfxCommon.SetFloat(_electricitySize, "ElectricitySize");

        //Debug.Log(_nowState);
    }

    void Appear()
    {
        if (_nowState != GateState.Appear) return;

        _sphereSize = _vfxCommon.GetFloat("SphereSize");
        if (_sphereSize < _maxSphereSize)
        {
            _sphereSize += _growSpeed * Time.deltaTime;
        }
        else
        {
            _sphereSize = _maxSphereSize;
            _vfxCommon.SetBool(true, "IsRimLight");
            _nowState = GateState.Open;
        }
        _vfxCommon.SetFloat(_sphereSize, "SphereSize");

        // �d�C�̑傫��
        _electricitySize = _sphereSize * 2;

        // �f�B�]���u
        _dissolveThreshold -= _dissolveSpeed * Time.deltaTime;
        if (_dissolveThreshold <= 0) _dissolveThreshold = 0;
        _vfxCommon.SetFloat(_dissolveThreshold, "DissolveThreshold");
    }

    void Open()
    {
        if (_nowState != GateState.Open) return;

        if (_isCloseGate)
        {
            _rimLightProgress = 0;
            _nowState = GateState.Disappear;
        }

        // �d�C�̑傫��    ���a + ����傫��
        _electricitySize = _sphereSize * 2 + _sphereSize * _electricitySizeOffset;

        _rimLightProgress += 1 / _rimLightTakeTime * Time.deltaTime;
        _vfxCommon.SetColor(Color.Lerp(new Color(), _rimLightColor, _rimLightProgress), "RimLightColor");
    }

    void Disappear()
    {
        if (_nowState != GateState.Disappear) return;

        if (_dissolveThreshold >= 1.0f)
        {

            _sphereSize = 0;
            _vfxCommon.SetFloat(_sphereSize, "SphereSize");
            _electricitySize = 0;
            _vfxCommon.SetFloat(_electricitySize, "ElectricitySize");
            _dissolveThreshold = 1;
            _vfxCommon.SetFloat(_dissolveThreshold, "DissolveThreshold");
            _electricityProgress = 0;
            _rimLightProgress = 0;

            _vfxCommon.SetBool(false, "IsRimLight");

            // �������C�g�͍ŏ��͓���
            Color color = new Color();
            _vfxCommon.SetColor(color, "RimLightColor");

            gameObject.SetActive(false);
            return;
        }

        // �d�C�̑傫��
        _electricitySize = Mathf.Lerp(_sphereSize * 2 + _sphereSize * _electricitySizeOffset, 0, _electricityProgress);
        _electricityProgress += 1 / _electricityTakeTime * Time.deltaTime;

        // �f�B�]���u
        _dissolveThreshold += _dissolveSpeed * Time.deltaTime;
        if (_dissolveThreshold >= 1) _dissolveThreshold = 1;
        _vfxCommon.SetFloat(_dissolveThreshold, "DissolveThreshold");

        // �������C�g
        _vfxCommon.SetColor(Color.Lerp(_rimLightColor, new Color(), _rimLightProgress), "RimLightColor");
        _rimLightProgress += 1 / _rimLightTakeTime * Time.deltaTime;
    }

    public void CloseGate()
    {
        _isCloseGate = true;
    }
}
