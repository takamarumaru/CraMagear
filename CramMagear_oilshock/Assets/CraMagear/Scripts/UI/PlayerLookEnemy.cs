using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class PlayerLookEnemy : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject player;
    [SerializeField] GameObject arrow;
    //[SerializeField] GameObject Rotate;  //プレイヤーの回転
    private float searchTime = 0;    //経過時間

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = player.transform.position + player.transform.forward * 1.2f + Vector3.up * 0.1f;
        Vector2 vec3 = new Vector2(target.transform.position.x - player.transform.position.x,
                                   target.transform.position.z - player.transform.position.z);

        float r = Mathf.Atan2(vec3.y,vec3.x);
        float angle = Mathf.Floor(r * 360 / ( 2 * Mathf.PI));
        //gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        arrow.transform.rotation = Quaternion.Euler(0, 90 - angle, 0);
        //Vector3 _PlayerP = transform.parent.transform.localRotation.eulerAngles;
        //Vector3 result = transform.localRotation.eulerAngles;


    }

    
}
