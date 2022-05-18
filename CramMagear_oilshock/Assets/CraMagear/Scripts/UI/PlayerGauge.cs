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
        var valueFrom = life / maxLife;                     //現在のHP / 最大HP
        var valueTo = (life - reducationValue) / maxLife;   //(現在のHP - 割り引き) / 最大HP

        //緑ゲージ減少
        GreenGage.fillAmount = valueTo;

        if (redGaugeTween != null)
        {
            redGaugeTween.Kill();
        }

        // 赤ゲージ減少
        //redGaugeTween = DOTween.To(             
        //    () => valueFrom,                    //何を対象にするのか
        //    x => { RedGage.fillAmount = x; },   //値の更新
        //    valueTo,                            //最終的な値
        //    time                                //アニメーション時間
        //);

        RedGage.DOFillAmount(valueTo, time);
    }

}