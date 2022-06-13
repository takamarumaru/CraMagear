using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MudBulletUI : MonoBehaviour
{
    [SerializeField]
    BulletChangeCount m_BulletChangeCount;

    [SerializeField]
    Image MudBullet;

    [SerializeField]
    Image MudMask;

    [SerializeField]
    private float SkillRecastTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_BulletChangeCount.count == 3)
        {
            MudBullet.enabled = true;
            MudMask.enabled = true;
            //MudBullet.color = new Color(1, 1, 1, 1);
            //MudMask.color = new Color(1, 1, 1, 1);

            if (!Input.GetKeyDown(KeyCode.Z))
            {
                MudMask.fillAmount -= 1.0f / SkillRecastTime * Time.deltaTime;
            }
            else
            {
                if (MudMask.fillAmount <= 0.0f)
                {
                    MudMask.fillAmount = 1.0f;

                }
            }
        }
        else
        {
            MudBullet.enabled = false;
            MudMask.enabled = false;
            //MudBullet.color = new Color(1, 1, 1, 0);
            //MudMask.color = new Color(1, 1, 1, 0);
            if (!Input.GetKeyDown(KeyCode.Z))
            {
                MudMask.fillAmount -= 1.0f / SkillRecastTime * Time.deltaTime;
            }
            /*else
            {
                if (MudMask.fillAmount <= 0.0f)
                {
                    MudMask.fillAmount = 1.0f;

                }
            }*/
            
        }
    }
}
