using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterSeconds : MonoBehaviour
{
    [SerializeField] private float _aliveTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _aliveTime);
    }
}
