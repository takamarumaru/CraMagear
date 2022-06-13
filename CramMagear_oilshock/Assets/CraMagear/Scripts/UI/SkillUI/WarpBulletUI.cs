using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarpBulletUI : MonoBehaviour
{
    [SerializeField]
    BulletChangeCount m_BulletChangeCount;

    [SerializeField]
    Image WarpBullet;

    [SerializeField]
    Image WarpMask;

    [SerializeField]
    private float SkillRecastTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_BulletChangeCount.count == 4)
        {
            WarpBullet.enabled = true;
            WarpMask.enabled = true;
            //WarpBullet.color = new Color(1, 1, 1, 1);
            //WarpMask.color = new Color(1, 1, 1, 1);

            if (!Input.GetKeyDown(KeyCode.Z))
            {
                WarpMask.fillAmount -= 1.0f / SkillRecastTime * Time.deltaTime;
            }
            else
            {
                if (WarpMask.fillAmount <= 0.0f)
                {
                    WarpMask.fillAmount = 1.0f;

                }
            }
        }
        else
        {
            WarpBullet.enabled = false;
            WarpMask.enabled = false;
            //WarpBullet.color = new Color(1, 1, 1, 0);
            //WarpMask.color = new Color(1, 1, 1, 0);
            if (!Input.GetKeyDown(KeyCode.Z))
            {
                WarpMask.fillAmount -= 1.0f / SkillRecastTime * Time.deltaTime;
            }
            /*else
            {
                if (WarpMask.fillAmount <= 0.0f)
                {
                    WarpMask.fillAmount = 1.0f;

                    
                }
            }*/
            
        }
    }
}
