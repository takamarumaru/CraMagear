using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pase1Timer : MonoBehaviour
{
    [SerializeField]
    GameAadministrator CountLimitTime;

    public static float CountDownTime;
    public Text TextCountDown;
    //[SerializeField]
    //public Image countDownImage;
    // Start is called before the first frame update
    void Start()
    {
        CountDownTime = CountLimitTime.FirstPhaseTime;
    }

    // Update is called once per frame
    void Update()
    {
        TextCountDown.text = string.Format("Phase1  {0:00:00}", CountDownTime);
	    //countDownImage.fillAmount = CountDownTime % 1.0f;

	    CountDownTime -= Time.deltaTime;

	    if(CountDownTime <= 0)
	    {
		    CountDownTime = 0;
	    }
    }
}
