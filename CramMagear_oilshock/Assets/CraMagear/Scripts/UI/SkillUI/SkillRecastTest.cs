using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillRecastTest : MonoBehaviour
{
    [SerializeField]
    public Image SkillImage;
    [SerializeField]
    public Image SkillMaskImage;

    [SerializeField]
    public float SkillRecastTime;
    bool pushFlag = false;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = SkillRecastTime;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (!Input.GetKeyDown(KeyCode.Z))
        {
            SkillMaskImage.fillAmount -= 1.0f / SkillRecastTime * Time.deltaTime;
        }
        else
        {
            if(SkillMaskImage.fillAmount <= 0.0f)
            {
                SkillMaskImage.fillAmount = 1.0f;
                
            }
        }
    }
}
