using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PhaseTimer : MonoBehaviour
{
    //�@�������ԁi���j
    //[SerializeField]
    //private int minute;
    //�@�������ԁi�b�j
    //[SerializeField]
    //private float seconds;
    [SerializeField]
    GameAadministrator CountLimitTime;

    public static float CountDownTime;    // �J�E���g�_�E���^�C��
    public Text TextCountDown;              // �\���p�e�L�X�gUI
    [SerializeField]
    public Image countDownImage;
    // Start is called before the first frame update
    void Start()
    {
        CountDownTime = CountLimitTime.FirstPhaseTime;
        //CountDownTime = GameAadministrator.Instance.FirstPhaseCount;
        //CountDownTime = 60.0F;    // �J�E���g�_�E���J�n�b�����Z�b�g
    }

    // Update is called once per frame
    void Update()
    {
       
        // �J�E���g�_�E���^�C���𐮌`���ĕ\��
        TextCountDown.text = string.Format("Phase1: {0:00:00}", CountDownTime);
        countDownImage.fillAmount = CountDownTime % 1.0f; ;

        // �o�ߎ����������Ă���
        CountDownTime -= Time.deltaTime;
        
        if(CountDownTime <= 0)
        {
            CountDownTime = 0;
        }
    }
}
