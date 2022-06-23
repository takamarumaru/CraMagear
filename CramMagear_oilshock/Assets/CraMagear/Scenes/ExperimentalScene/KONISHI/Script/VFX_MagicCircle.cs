using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(VFX_Common))]

public class VFX_MagicCircle : MonoBehaviour
{
    private VFX_Common _vfxCommon;
    public VFX_Common VFXCommon => _vfxCommon;

    [SerializeField]
    Layer _layer;

    [SerializeField]
    VisualEffect _vfxAura;

    private float _offsetAngle = 0;
    private float _angle = 0;
    private float _addAngle = 0;


    public void Initialize(VFX_MagicCircle obj, Layer layer)
    {
        obj._vfxCommon = gameObject.GetComponent<VFX_Common>();
        obj.VFXCommon.SetTexture(layer.Texture);
        obj.VFXCommon.SetFloat(layer.Scale, "Scale");
        obj.VFXCommon.SetGradient(layer.Gradient);
        obj.VFXCommon.SetFloat(layer.AngularVelocity, "AngularVelocity");
        obj.VFXCommon.SetFloat(layer.Radius, "Radius");
        obj.VFXCommon.SetFloat(layer.OffsetAngle, "GlobalAngle");
        obj.VFXCommon.SetFloat(layer.OffsetAngleSpeed, "GlobalAngleSpeed");

        _layer = layer;
    }

    void Awake()
    {
        _vfxCommon = transform.GetComponent<VFX_Common>();
    }

    void Update()
    {
        _offsetAngle = _vfxCommon.GetFloat("GlobalAngle");
        _addAngle = _vfxCommon.GetFloat("GlobalAngleSpeed");

        _angle += _addAngle * Time.deltaTime;
        if (_angle >= 360) _angle -= 360;
        if (_angle < 0) _angle += 360;

        // 複数生成する場合に、円周上に等間隔に並べる
        foreach (var str in _vfxCommon.name.Split('_'))
        {
            int outNum;
            if (int.TryParse(str, out outNum))
            {
                _offsetAngle += 360.0f / _layer.InstanceNum * outNum;
            }
        }

        float angle = (_offsetAngle + _angle) * Mathf.Deg2Rad;
        Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * Mathf.Abs(_vfxCommon.GetFloat("Radius"));
        //_vfxCommon.transform.position = pos;
        _vfxCommon.SetVector3(pos, "LocalPos");

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    SetScele(2.0f);
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    SetScele(0.5f);
        //}
    }

    void SetScele(float s)
    {
        _vfxCommon.transform.localScale *= s;
        _vfxCommon.SetFloat(_vfxCommon.GetFloat("Radius") * s, "Radius");
    }
}


[System.Serializable]
public class Layer
{
    [SerializeField, Header("画像以外のパラメータを1つ上の要素からコピーする")]
    bool isCopy;
    public bool IsCopy => isCopy;

    [SerializeField, Header("画像")]
    Texture texture;
    public Texture Texture => texture;

    [SerializeField, Header("配置する数(同一円上に等間隔)")]
    public uint instanceNum;
    public uint InstanceNum => instanceNum;

    [SerializeField, Header("拡大率")]
    float scale;
    public float Scale => scale;

    [SerializeField, Header("色")]
    Gradient gradient;
    public Gradient Gradient => gradient;

    [SerializeField, Header("角速度(deg/s)")]
    float angularVelocity;
    public float AngularVelocity => angularVelocity;

    [SerializeField, Header("中心から画像の基準点の距離")]
    float radius;
    public float Radius => radius;

    [SerializeField, Header("円周上の初期角度(位置)")]
    float offsetAngle;
    public float OffsetAngle => offsetAngle;

    [SerializeField, Header("円周上の回転速度(deg/s)")]
    float offsetAngleSpeed;
    public float OffsetAngleSpeed => offsetAngleSpeed;

    public void Initialize(Layer layer)
    {
        // この2つはコピーしない
        //isCopy = layer.IsCopy;
        //texture = layer.Texture;

        // 他はコピーする
        instanceNum = layer.InstanceNum;
        scale = layer.Scale;
        gradient = layer.Gradient;
        angularVelocity = layer.AngularVelocity;
        radius = layer.Radius;
        offsetAngle = layer.OffsetAngle;
        offsetAngleSpeed = layer.OffsetAngleSpeed;
    }
}