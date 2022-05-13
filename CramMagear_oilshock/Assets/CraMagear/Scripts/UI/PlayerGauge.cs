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

    private SamplePlayerHP player;
    private Tween redGaugeTween;

    float _hp = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GaugeReduction(float reducationValue, float time = 1f)
    {
        var valueFrom = player.life / player.maxLife;
        var valueTo = (player.life - reducationValue) / player.maxLife;

        // �΃Q�[�W����
        GreenGage.fillAmount = valueTo;

        if (redGaugeTween != null)
        {
            redGaugeTween.Kill();
        }

        // �ԃQ�[�W����
        redGaugeTween = DOTween.To(
            () => valueFrom,
            x => {
                RedGage.fillAmount = x;
            },
            valueTo,
            time
        );
    }

    public void Update()
    {
        
        //// HP�㏸
        //_hp += 0.01f;
        //if (_hp > 1)
        //{
        //    // �ő�𒴂�����0�ɖ߂�
        //    _hp = 0;
        //}
    }

    public void HPDown(float current, int max)
    {
        //�R���|�[�l���g��fillAmount���擾���đ��삷��
        GreenGage.GetComponent<Image>().fillAmount = current / max;
    }
    
}
