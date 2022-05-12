using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SamplePlayerHP : MonoBehaviour
{
    //test
    public float life;
    public float maxLife;
    public float HP = 100;
    //���݂̎���
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
        // �O�̃t���[������o�߂����b�������Z
        currentTime += Time.deltaTime;

        //���b�������s��
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
