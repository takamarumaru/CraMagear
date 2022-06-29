using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VFX_Common))]

public class ProjectileMotion : MonoBehaviour
{
    private VFX_Common _vfxCommon;

    [SerializeField] GameObject _player;
    [SerializeField] GameObject _camera;

    [SerializeField, Header("ìñÇΩÇËîªíËÇÃä‘äu(ïb)")]
    float _hitCheckDeltaTime;

    [SerializeField, Header("é¿ç€Ç…îÚÇŒÇ∑ä‘äu(ïb)")]
    float _attackDeltaTime;

    [SerializeField, Header("ÉåÉCÇÃç≈ëÂêî")]
    uint _maxHitCheckRayNum;

    [SerializeField, Header("èâë¨ìx")]
    float _velocity;

    [SerializeField, Header("èdóÕâ¡ë¨ìx")]
    float _gravity;

    [SerializeField]
    GameObject _rayPrafab;

    [SerializeField]
    Vector3 _forward = Vector3.forward;

    [SerializeField, Header("èeå˚ÇÃà íuí≤êÆ")]
    Vector3 _offsetPos;

    [Header("ÉfÉoÉbÉOóp")]
    [SerializeField]
    bool _isDrawHitCheckLine = true;
    [SerializeField]
    bool _isDrawHitCheckSphere = true;
    [SerializeField]
    bool _isDrawAttackLine = true;
    [SerializeField]
    bool _isDrawAttackSphere = true;

    float _cameraAngleX = 0;
    float _playerAngleY = 0;

    int _attackRayDuration = 0;
    int _hitIndex = 0;
    uint _maxAttackRayNum = 0;
    bool _isHitAlready = false;
    Vector3 _hitPos = new Vector3();
    int _attackActiveIndex = 0;

    GameObject _parentRayObj;
    List<GameObject> _childRayObjs = new List<GameObject>();

    List<Vector3> _hitCheckRayMiddlePoses = new List<Vector3>();
    List<Vector3> _attackRayMiddlePoses = new List<Vector3>();

    public void GetParameters(out Vector3 appearPos, out float angleX, out float angleY, out float velocity, out float gravity, out float takeTime)
    {
        GameObject obj = new GameObject();
        obj.transform.position = _offsetPos;
        obj.transform.RotateAround(Vector3.zero, Vector3.up, _player.transform.localEulerAngles.y);

        appearPos = _player.transform.position + obj.transform.position;
        angleX = _cameraAngleX;
        angleY = _playerAngleY;
        velocity = _velocity;
        gravity = _gravity;
        takeTime = _attackDeltaTime * _attackActiveIndex;

        Destroy(obj);
    }

    private void Awake()
    {
        _vfxCommon = transform.GetComponent<VFX_Common>();

        _parentRayObj = new GameObject("VFXRay");

        _attackRayDuration = (int)(_hitCheckDeltaTime / _attackDeltaTime);

        _maxAttackRayNum = (uint)_attackRayDuration * _maxHitCheckRayNum + 1;
        _maxHitCheckRayNum++;

        for (int i = 0; i < _maxHitCheckRayNum; i++) _hitCheckRayMiddlePoses.Add(new Vector3());
        for (int i = 0; i < _maxAttackRayNum; i++) _attackRayMiddlePoses.Add(new Vector3());
        for (int i = 0; i < _maxAttackRayNum; i++) _childRayObjs.Add(Instantiate(_rayPrafab, _parentRayObj.transform));
        for (int i = 0; i < _maxAttackRayNum; i++) _childRayObjs[i].name = "VFXRay" + i;
    }

    private void Update()
    {
        _cameraAngleX = -_camera.transform.localEulerAngles.x * Mathf.Deg2Rad;            // Xé≤âÒì]äpìx
        _playerAngleY = (-_player.transform.localEulerAngles.y + 90) * Mathf.Deg2Rad;     // Yé≤âÒì]äpìx

        CalculatePos(out _hitCheckRayMiddlePoses, _hitCheckDeltaTime, (int)_maxHitCheckRayNum, _cameraAngleX, _playerAngleY);
        CalculatePos(out _attackRayMiddlePoses, _attackDeltaTime, (int)_maxAttackRayNum, _cameraAngleX, _playerAngleY);

        // ìñÇΩÇËîªíËÇÃÉåÉC
        DrawRay(ref _hitCheckRayMiddlePoses, Color.red, Color.yellow, true, _isDrawHitCheckLine, _isDrawHitCheckSphere);

        // é¿ç€ÇÃãOìπÇÃÉåÉC
        DrawRay(ref _attackRayMiddlePoses, Color.blue, Color.green, false, _isDrawAttackLine, _isDrawAttackSphere);

        CalculateAttackRay();

        DrawAttackLocus();

        _vfxCommon.SetVector3(_hitPos, "Pos");
    }

    void CalculatePos(
        out List<Vector3> middlePoses,
        float deltaTime,
        int maxRayNum,
        float angleX,
        float angleY
        )
    {
        middlePoses = new List<Vector3>();

        GameObject obj = new GameObject();
        obj.transform.position = _offsetPos;
        obj.transform.RotateAround(Vector3.zero, Vector3.up, _player.transform.localEulerAngles.y);
        //DebugLine.DrawWireSphere(_player.transform.position + obj.transform.position, 0.1f, Color.red);

        for (int i = 0; i < maxRayNum; i++)
        {
            float X = deltaTime * i;

            Vector3 tmpPos = _player.transform.position + obj.transform.position;
            tmpPos += Calculate(X, angleX, angleY, _velocity, _gravity);

            middlePoses.Add(tmpPos);
        }

        Destroy(obj);
    }

    public Vector3 Calculate(float time, float angleX, float angleY, float velocity, float gravity)
    {
        float radius = velocity * time * Mathf.Cos(angleX);

        return new Vector3(radius * Mathf.Cos(angleY), -0.5f * gravity * time * time + velocity * time * Mathf.Sin(angleX), radius * Mathf.Sin(angleY));
    }

    void DrawRay(
        ref List<Vector3> middlePoses,
        Color startColor,
        Color endColor,
        bool isHitCheck = false,
        bool isDrawLine = true,
        bool isDrawSphere = true
        )
    {
        int rayNum = middlePoses.Count;

        // ÉåÉCÇç≈ëÂêîÇ‹Ç≈îÚÇŒÇ∑
        for (int i = 0; i < rayNum; i++)
        {
            Color color = Color.Lerp(startColor, endColor, (float)i / rayNum);

            if (i > 0)
            {
                if (isHitCheck)
                {
                    Vector3 dir = middlePoses[i] - middlePoses[i - 1];
                    Ray ray = new Ray(middlePoses[i - 1], dir);
                    if (Physics.Raycast(ray, out RaycastHit hit, dir.magnitude))
                    {
                        if (!_isHitAlready)
                        {
                            _isHitAlready = true;
                            _hitIndex = i;
                            _hitPos = hit.point;

                            if (isDrawLine)
                            {
                                DebugLine.DrawWireSphere(hit.point, 0.1f, color);
                                //Debug.DrawRay(ray.origin, dir, Color.red, 0.1f);
                            }
                        }
                    }
                }

                if (isDrawLine)
                {
                    Debug.DrawLine(middlePoses[i - 1], middlePoses[i], color);
                }
            }

            if (isDrawSphere)
            {
                DebugLine.DrawWireSphere(middlePoses[i], 0.03f, color);
            }
        }
    }

    void CalculateAttackRay()
    {
        GameObject obj = new GameObject();
        obj.transform.position = _offsetPos;
        obj.transform.RotateAround(Vector3.zero, Vector3.up, _player.transform.localEulerAngles.y);
        Vector3 playerPos = _player.transform.position + obj.transform.position;

        for (int i = _attackRayDuration * (_hitIndex - 1); i < _attackRayDuration * _hitIndex; i++)
        {
            if ((_hitPos - playerPos).sqrMagnitude >= (_attackRayMiddlePoses[i] - playerPos).sqrMagnitude)
            {
                if ((_hitPos - playerPos).sqrMagnitude <= (_attackRayMiddlePoses[i + 1] - playerPos).sqrMagnitude)
                {
                    _attackActiveIndex = i + 1;
                }
            }
        }

        Destroy(obj);
    }

    /// <summary>
    /// çUåÇÇÃãOê’Çï\é¶
    /// </summary>
    void DrawAttackLocus()
    {
        for (int i = 0; i < _maxAttackRayNum; i++)
        {
            _childRayObjs[i].transform.position = _attackRayMiddlePoses[i];

            if (i > 0)
            {
                var dir = _childRayObjs[i - 1].transform.position - _childRayObjs[i].transform.position;
                var lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);
                var offsetRotation = Quaternion.FromToRotation(_forward, Vector3.forward);

                _childRayObjs[i - 1].transform.rotation = lookAtRotation * offsetRotation;
            }

            // Yé≤ï˚å¸ÇÃägèkÇèCê≥Ç∑ÇÈ
            if (i < _maxAttackRayNum - 1)
            {
                var dir = _childRayObjs[i + 1].transform.position - _childRayObjs[i].transform.position;

                Vector3 scale = _childRayObjs[i].transform.localScale;
                scale.y = dir.magnitude;
                _childRayObjs[i].transform.localScale = scale;
            }

            _childRayObjs[i].SetActive(i < _attackActiveIndex);
        }

        _isHitAlready = false;
    }
}
