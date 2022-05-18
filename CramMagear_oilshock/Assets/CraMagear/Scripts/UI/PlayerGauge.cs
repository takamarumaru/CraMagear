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

    //private Tween greenGaugeTween;

    [SerializeField]
    public float life;
    [SerializeField]
    public float maxLife;

    //public Image image;

    // Start is called before the first frame update
    void Start()
    {
        Sequence sequence = DOTween.Sequence();
    }

    void Update()
    {
        float power = 10.0f;

        GaugeReduction(power);
        if (Input.GetKeyDown(KeyCode.H))
        {
            life -= power;
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

}