using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGauge : MonoBehaviour
{
    [SerializeField]
    private Image GreenGage;
    [SerializeField]
    private Image RedGage;

    private SamplePlayerHP player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GaugeReduction(float reducationValue, float time = 1f)
    {
        var valueFrom = player.life / player.maxLife;
        var valueTo = (player.life - reducationValue) / player.maxLife;

        // óŒÉQÅ[ÉWå∏è≠
        GreenGage.fillAmount = valueTo;
    }

    public void SetPlayer(SamplePlayerHP player)
    {
        this.player = player;
    }
}
