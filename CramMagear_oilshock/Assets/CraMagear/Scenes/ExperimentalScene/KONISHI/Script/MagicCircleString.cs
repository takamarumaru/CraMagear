using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MagicCircleString
{
    [SerializeField, Header("•¶š—ñ")]
    string str;
    public string Str => str;

    [SerializeField, Header("”z’u‚·‚é”(“¯ˆê‰~ã‚É“™ŠÔŠu)")]
    uint instanceNum;
    public uint InstanceNum => instanceNum;

    [SerializeField, Header("Šg‘å—¦")]
    float scale;
    public float Scale => scale;

    [SerializeField, Header("•¶š‚ÌŠî€“_")]
    TextAnchor _ancor;
    public TextAnchor Ancor => _ancor;

    [SerializeField, Header("F")]
    Color color;
    public Color Color => color;

    [SerializeField, Header("’†S‚©‚ç‚Ì‹——£")]
    float radius;
    public float Radius => radius;

    [SerializeField, Header("‰~üã‚Ì‰ŠúŠp“x(ˆÊ’u)")]
    float offsetAngle;
    public float OffsetAngle => offsetAngle;

    [SerializeField, Header("g‚¤Šp“x‚Ì”ÍˆÍ(•Ğ‘¤)"), Range(0.0f, 180.0f)]
    float angleRange;
    public float AngleRange => angleRange;

    [SerializeField, Header("‰~üã‚Ì‰ñ“]‘¬“x(deg/s)")]
    float offsetAngleSpeed;
    public float OffsetAngleSpeed => offsetAngleSpeed;

    public static int FontSizeMax = 500;
}

public class MagicCircleStringUpdate : MonoBehaviour
{
    [SerializeField, Header("‰ñ“]‚Ì’†S")]
    Transform _rotatePivot;

    [SerializeField, Header("‰ñ“]‚Ì‘¬‚³(deg/s)")]
    float _rotateSpeed;

    public void Initialize(Transform rotatePivot, float rotateSpeed)
    {
        _rotatePivot = rotatePivot;
        _rotateSpeed = rotateSpeed;
    }

    void Update()
    {
        transform.RotateAround(_rotatePivot.position, _rotatePivot.up, -_rotateSpeed * Time.deltaTime);
    }
}
