using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VFX_Common))]

public class VFX_EnemySpawn : MonoBehaviour
{
    private VFX_Common _vfxCommon;

    [SerializeField, Header("最大になるまでの時間")]
    float _sphereSizeGrowTime = 1.0f;

    [SerializeField, Header("電気エフェクトの大きさ")]
    float _electricitySizeOffset = 1.0f;

    [SerializeField, Header("ディゾルブで消えるまでの時間")]
    float _dissolveTakeTime = 1.0f;

    bool _isCloseGate = false;

    // Sphere
    float _sphereSize;              // Sphereの今の大きさ
    float _maxSphereSize;           // Sphereの最大の大きさ
    float _growSpeed;               // Sphereが大きくなる速さ

    // 電気エフェクト
    float _electricitySize;         // 電気のエフェクトの大きさ
    float _electricityTakeTime = 1; // 消滅時に掛かる時間
    float _electricityProgress;

    // ディゾルブ
    float _dissolveThreshold = 1;   // ディゾルブの度合い
    float _dissolveSpeed;           // ディゾルブの速さ

    // リムライト
    Color _rimLightColor;
    float _rimLightTakeTime = 2;    // リムライトに掛かる時間
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

        // リムライトは最初は透明
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

        // 電気の大きさ
        _electricitySize = _sphereSize * 2;

        // ディゾルブ
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

        // 電気の大きさ    直径 + 一回り大きく
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

            // リムライトは最初は透明
            Color color = new Color();
            _vfxCommon.SetColor(color, "RimLightColor");

            gameObject.SetActive(false);
            return;
        }

        // 電気の大きさ
        _electricitySize = Mathf.Lerp(_sphereSize * 2 + _sphereSize * _electricitySizeOffset, 0, _electricityProgress);
        _electricityProgress += 1 / _electricityTakeTime * Time.deltaTime;

        // ディゾルブ
        _dissolveThreshold += _dissolveSpeed * Time.deltaTime;
        if (_dissolveThreshold >= 1) _dissolveThreshold = 1;
        _vfxCommon.SetFloat(_dissolveThreshold, "DissolveThreshold");

        // リムライト
        _vfxCommon.SetColor(Color.Lerp(_rimLightColor, new Color(), _rimLightProgress), "RimLightColor");
        _rimLightProgress += 1 / _rimLightTakeTime * Time.deltaTime;
    }

    public void CloseGate()
    {
        _isCloseGate = true;
    }
}
