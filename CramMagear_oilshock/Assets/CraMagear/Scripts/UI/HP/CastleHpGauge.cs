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

    //[Header("プレイヤー")]
    //[SerializeField]
    //Transform targetPlayer;

    bool _active = false;   //画像のオンオフ

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

        //カメラのレイ判定
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //LayerMask mask = LayerMask.GetMask("StageMap");

        //内積取得
        //float angle = GetAngle(targetPlayer.position, _CastleParameter.transform.position);

        //城からカメラの方向へ正規化したベクトルを作成
        Vector3 CastleToCameraDir = (_camera.position - _CastleParameter.transform.position).normalized;
        //正規化したベクトルの内積が一定以下なら見たことにする
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
