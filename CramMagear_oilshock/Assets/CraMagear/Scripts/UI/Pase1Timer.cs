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
        TextCountDown.text = string.Format(CountLimitTime.State.ToString() + "\n{0:00:00}", CountLimitTime.PhaseCount);
	    //countDownImage.fillAmount = CountDownTime % 1.0f;
    }
}
