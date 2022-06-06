using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PhaseTimer : MonoBehaviour
{
    //　制限時間（分）
    //[SerializeField]
    //private int minute;
    //　制限時間（秒）
    //[SerializeField]
    //private float seconds;
    [SerializeField]
    GameAadministrator CountLimitTime;

    public static float CountDownTime;    // カウントダウンタイム
    public Text TextCountDown;              // 表示用テキストUI
    [SerializeField]
    public Image countDownImage;
    // Start is called before the first frame update
    void Start()
    {
        CountDownTime = CountLimitTime.FirstPhaseTime;
        //CountDownTime = GameAadministrator.Instance.FirstPhaseCount;
        //CountDownTime = 60.0F;    // カウントダウン開始秒数をセット
    }

    // Update is called once per frame
    void Update()
    {
       
        // カウントダウンタイムを整形して表示
        TextCountDown.text = string.Format("Phase1: {0:00:00}", CountDownTime);
        countDownImage.fillAmount = CountDownTime % 1.0f; ;

        // 経過時刻を引いていく
        CountDownTime -= Time.deltaTime;
        
        if(CountDownTime <= 0)
        {
            CountDownTime = 0;
        }
    }
}
