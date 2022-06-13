using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletChangeCount : MonoBehaviour
{
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
    }
}
