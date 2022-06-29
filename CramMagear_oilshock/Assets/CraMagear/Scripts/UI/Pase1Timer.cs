using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pase1Timer : MonoBehaviour
{
    [SerializeField]
    GameAadministrator CountLimitTime;

    public Text TextCountDown;


    void Update()
    {
        TextCountDown.text = string.Format(CountLimitTime.State.ToString() + "\n" + ((int)CountLimitTime.PhaseCount).ToString());
	    //countDownImage.fillAmount = CountDownTime % 1.0f;
    }
}
