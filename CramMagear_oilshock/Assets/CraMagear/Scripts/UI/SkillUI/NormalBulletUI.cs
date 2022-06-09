using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalBulletUI : MonoBehaviour
{
    [SerializeField]
    BulletChangeCount m_BulletChangeCount;

    [SerializeField]
    Image NormalBullet;

    [SerializeField]
    Image NormalMask;

    [SerializeField]
    private float SkillRecastTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_BulletChangeCount.count == 1)
        {
            NormalBullet.enabled = true;
            NormalMask.enabled = true;

            //NormalBullet.color = new Color(1, 1, 1, 1);
            //NormalMask.color = new Color(1, 1, 1, 1);

            if (!Input.GetKeyDown(KeyCode.Z))
            {
                NormalMask.fillAmount -= 1.0f / SkillRecastTime * Time.deltaTime;
            }
            else
            {
                if (NormalMask.fillAmount <= 0.0f)
                {
                    NormalMask.fillAmount = 1.0f;

                }
            }
        }
        else
        {
            NormalBullet.enabled = false;
            NormalMask.enabled = false;
            //NormalBullet.color = new Color(1, 1, 1, 0);
            //NormalMask.color = new Color(1, 1, 1, 0);
            if (!Input.GetKeyDown(KeyCode.Z))
            {
                NormalMask.fillAmount -= 1.0f / SkillRecastTime * Time.deltaTime;
            }
            /*else
            {
                if (NormalMask.fillAmount <= 0.0f)
                {
                    NormalMask.fillAmount = 1.0f;

                }
            }*/
            
        }
    }
}
