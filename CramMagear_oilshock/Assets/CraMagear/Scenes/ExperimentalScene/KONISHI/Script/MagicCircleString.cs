using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MagicCircleString
{
    [SerializeField, Header("������")]
    string str;
    public string Str => str;

    [SerializeField, Header("�z�u���鐔(����~��ɓ��Ԋu)")]
    uint instanceNum;
    public uint InstanceNum => instanceNum;

    [SerializeField, Header("�g�嗦")]
    float scale;
    public float Scale => scale;

    [SerializeField, Header("�����̊�_")]
    TextAnchor _ancor;
    public TextAnchor Ancor => _ancor;

    [SerializeField, Header("�F")]
    Color color;
    public Color Color => color;

    [SerializeField, Header("���S����̋���")]
    float radius;
    public float Radius => radius;

    [SerializeField, Header("�~����̏����p�x(�ʒu)")]
    float offsetAngle;
    public float OffsetAngle => offsetAngle;

    [SerializeField, Header("�g���p�x�͈̔�(�Б�)"), Range(0.0f, 180.0f)]
    float angleRange;
    public float AngleRange => angleRange;

    [SerializeField, Header("�~����̉�]���x(deg/s)")]
    float offsetAngleSpeed;
    public float OffsetAngleSpeed => offsetAngleSpeed;

    public static int FontSizeMax = 500;
}

public class MagicCircleStringUpdate : MonoBehaviour
{
    [SerializeField, Header("��]�̒��S")]
    Transform _rotatePivot;

    [SerializeField, Header("��]�̑���(deg/s)")]
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
