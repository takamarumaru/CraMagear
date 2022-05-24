using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            transform.position += new Vector3(0, 0, 15.0f) * Time.deltaTime;
            if (transform.position.z > 25) transform.position = new Vector3(0, 0, -25);
        }
    }
}
