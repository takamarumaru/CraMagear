using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterSeconds : MonoBehaviour
{
    private float _aliveTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _aliveTime);
    }
}
