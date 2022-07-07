using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ThunderBulletUI : MonoBehaviour
{
    [SerializeField]
    BulletChangeCount m_BulletChangeCount;

    [SerializeField]
    Image ThunderBullet;

    [SerializeField]
    Image ThunderMask;

    [SerializeField]
    private float SkillRecastTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_BulletChangeCount.count == 2)
        {
            ThunderBullet.enabled = true;
            ThunderMask.enabled = true;
            //ThunderBullet.color = new Color(1, 1, 1, 1);
            //ThunderMask.color = new Color(1, 1, 1, 1);

            if (!PlayerInputManager.Instance.GamePlay_GetButtonAttack())
            {
                ThunderMask.fillAmount -= 1.0f / SkillRecastTime * Time.deltaTime;
            }
            else
            {
                if (ThunderMask.fillAmount <= 0.0f)
                {
                    ThunderMask.fillAmount = 1.0f;

                }
            }
        }
        else
        {
            ThunderBullet.enabled = false;
            ThunderMask.enabled = false;
            //ThunderBullet.color = new Color(1, 1, 1, 0);
            //ThunderMask.color = new Color(1, 1, 1, 0);
            if (!PlayerInputManager.Instance.GamePlay_GetButtonAttack())
            {
                ThunderMask.fillAmount -= 1.0f / SkillRecastTime * Time.deltaTime;
            }
            /*else
            {
                if (ThunderMask.fillAmount <= 0.0f)
                {
                    ThunderMask.fillAmount = 1.0f;

                }
            }*/
            
        }
    }
}
