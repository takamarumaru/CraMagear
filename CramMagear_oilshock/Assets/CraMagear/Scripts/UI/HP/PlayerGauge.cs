using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PlayerGauge : MonoBehaviour
{
    [SerializeField]
    private Image GreenGage;
    [SerializeField]
    private Image RedGage;

    private Tween redGaugeTween;

    [SerializeField]
    private MainObjectParameter _playerParameter;

    //private Tween greenGaugeTween;

    [SerializeField]
    private int life;
    [SerializeField]
    private  int maxLife;

    private int _prevFrameLife = 0;

    //public Image image;

    // Start is called before the first frame update
    void Start()
    {
        Sequence sequence = DOTween.Sequence();
        maxLife = _playerParameter.hp;
        life = _playerParameter.hp;
    }

    void Update()
    {
        if(_playerParameter.hp < _prevFrameLife)
        {
            GaugeReduction(_prevFrameLife - _playerParameter.hp);
            life = _playerParameter.hp;
        }

        _prevFrameLife = _playerParameter.hp;
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