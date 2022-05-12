using UnityEngine;

public class SamplePlayerHP : MonoBehaviour
{
    //test
    public float life;
    public float maxLife;
    public float HP = 100;
    //現在の時間
    private float currentTime = 0f;
    protected PlayerGauge playerGage;

    // Start is called before the first frame update
    void Start()
    {
        playerGage = GameObject.FindObjectOfType<PlayerGauge>();
        playerGage.SetPlayer(this);

        maxLife = HP;
    }

    void Update()
    {
        // 前のフレームから経過した秒数を加算
        currentTime += Time.deltaTime;

        //毎秒処理を行う
        if (currentTime >= 1.0f)
        {
            //HPbar.value += 10;
            currentTime = 0;
        }
    }

    public void Damage(float power)
    {
        playerGage.GaugeReduction(power);
        life -= power;
    }

}
