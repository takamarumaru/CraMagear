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
    public int life;
    [SerializeField]
    public int maxLife;

    private int _prevFrameLife = 0;

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
}
