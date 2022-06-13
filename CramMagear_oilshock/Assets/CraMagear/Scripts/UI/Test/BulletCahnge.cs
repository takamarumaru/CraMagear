using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCahnge : MonoBehaviour
{
    [SerializeField] Image Normal;
    [SerializeField] Image Thunder;
    [SerializeField] Image Mud;
    [SerializeField] Image Warp;

    public int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            count++;
            if (count > 4)
            {
                count = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            count--;
            if (count < 1)
            {
                count = 4;
            }
        }

        if(count == 1)
        {
            Normal.enabled = true;
            Thunder.enabled = false;
            Mud.enabled = false;
            Warp.enabled = false;
        }
        else if(count == 2)
        {
            Normal.enabled = false;
            Thunder.enabled = true;
            Mud.enabled = false;
            Warp.enabled = false;
        }
        else if (count == 3)
        {
            Normal.enabled = false;
            Thunder.enabled = false;
            Mud.enabled = true;
            Warp.enabled = false;
        }
        else
        {
            Normal.enabled = false;
            Thunder.enabled = false;
            Mud.enabled = false;
            Warp.enabled = true;
        }
    }
}
