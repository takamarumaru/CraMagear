using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTestPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.transform.Translate(1.0f, 0.0f, 0.0f);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            this.transform.Translate(0.0f, 0.0f, -1.0f);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.transform.Translate(0.0f, 0.0f, 1.0f);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.transform.Translate(-1.0f, 0.0f, 0.0f);
        }
    }
}
