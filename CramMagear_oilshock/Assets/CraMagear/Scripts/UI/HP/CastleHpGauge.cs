using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CastleHpGauge : MonoBehaviour
{
    [SerializeField]
    private Image GreenGage;
    [SerializeField]
    private Image RedGage;

    private Tween redGaugeTween;

    [SerializeField]
    private MainObjectParameter _CastleParameter;

    [SerializeField]
    private int life;
    [SerializeField]
    private int maxLife;

    private int _prevFrameLife = 0;

    [SerializeField]
    Transform _camera;

    //[Header("�v���C���[")]
    //[SerializeField]
    //Transform targetPlayer;

    bool _active = false;   //�摜�̃I���I�t

    float CheckAglee = -0.9f;

    RectTransform rect;
    

    void Start()
    {
        if(_active == false)
        {
            GreenGage.enabled = false;
            RedGage.enabled = false;
        }

        _camera = _camera.gameObject.GetComponent<Transform>();

        rect = GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector3(0, 275, 0);

    }

    // Start is called before the first frame update
    void Awake()
    {
        Sequence sequence = DOTween.Sequence();
        maxLife = _CastleParameter.hp;
        life = _CastleParameter.hp;
    }

    // Update is called once per frame
    void Update()
    {

        if (_CastleParameter.hp < _prevFrameLife)
        {
            GaugeReduction(_prevFrameLife - _CastleParameter.hp);
            life = _CastleParameter.hp;
        }

        _prevFrameLife = _CastleParameter.hp;

        //�J�����̃��C����
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //LayerMask mask = LayerMask.GetMask("StageMap");

        //���ώ擾
        //float angle = GetAngle(targetPlayer.position, _CastleParameter.transform.position);

        //�邩��J�����̕����֐��K�������x�N�g�����쐬
        Vector3 CastleToCameraDir = (_camera.position - _CastleParameter.transform.position).normalized;
        //���K�������x�N�g���̓��ς����ȉ��Ȃ猩�����Ƃɂ���
        if (Vector3.Dot(CastleToCameraDir, _camera.forward.normalized) < CheckAglee)
        {
            _active = true;
            if (_active == true)
            {
                GreenGage.enabled = true;
                RedGage.enabled = true;
                Invoke("Easing", 0.2f);

            }
        }
        else
        {
            rect.transform.DOLocalMoveY(275.0f, 0.2f).SetEase(Ease.Linear);

            _active = false;
            if (_active == false)
            {
                Invoke("FalseActive", 0.2f);
            }
        }
    }
    public void GaugeReduction(float reducationValue = 0.0f, float time = 1.0f)
    {
        var valueFrom = life / maxLife;                     //���݂�HP / �ő�HP
        var valueTo = (life - reducationValue) / maxLife;   //(���݂�HP - �������) / �ő�HP

        //�΃Q�[�W����
        GreenGage.fillAmount = valueTo;

        if (redGaugeTween != null)
        {
            redGaugeTween.Kill();
        }

        // �ԃQ�[�W����
        //redGaugeTween = DOTween.To(             
        //    () => valueFrom,                    //����Ώۂɂ���̂�
        //    x => { RedGage.fillAmount = x; },   //�l�̍X�V
        //    valueTo,                            //�ŏI�I�Ȓl
        //    time                                //�A�j���[�V��������
        //);

        RedGage.DOFillAmount(valueTo, time);
    }

    void FalseActive()
    {
        GreenGage.enabled = false;
        RedGage.enabled = false;
    }

    void Easing()
    {
        rect.transform.DOLocalMoveY(180.0f, 0.2f).SetEase(Ease.Linear);
    }
}
