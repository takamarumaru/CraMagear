using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class PlayerLookEnemy : MonoBehaviour
{
    [SerializeField] GameObject target1;
    [SerializeField] GameObject target2;
    [SerializeField] GameObject target3;
    [SerializeField] GameObject player;
    [SerializeField] GameObject arrow;
    //[SerializeField] GameObject Rotate;  //プレイヤーの回転
    private float searchTime = 0;    //経過時間
    bool isCalledOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        //if (target1.activeInHierarchy == true)
        //{
        //    Instantiate(arrow);
        //}
        //else if (target2.activeInHierarchy == true)
        //{
        //    Instantiate(arrow);
        //}
        //else
        //{
        //    Instantiate(arrow);
        //}
    }

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target1.activeInHierarchy == true)
        {
            //敵の方向に向く矢印のプレハブ作成
            //インスタンス化

            if(isCalledOnce == false)
            {
                isCalledOnce = true;
                Instantiate(arrow);
            }
            arrow.transform.position = player.transform.position + player.transform.forward * 1.0f + Vector3.up * 0.1f;
            Vector2 vec3 = new Vector2(target1.transform.position.x - player.transform.position.x,
                                       target1.transform.position.z - player.transform.position.z);
            float r = Mathf.Atan2(vec3.y, vec3.x);
            float angle = Mathf.Floor(r * 360 / (2 * Mathf.PI));
            arrow.transform.rotation = Quaternion.Euler(0, 90 - angle, 0);
        }
        else
        {
            Destroy(arrow);
        }


        if(target2.activeInHierarchy == true)
        {
            if (isCalledOnce == false)
            {
                isCalledOnce = true;
                Instantiate(arrow);
            }
            arrow.transform.position = player.transform.position + player.transform.forward * 1.0f + Vector3.up * 0.1f;
            Vector2 vec3 = new Vector2(target2.transform.position.x - player.transform.position.x,
                                       target2.transform.position.z - player.transform.position.z);
            float r = Mathf.Atan2(vec3.y, vec3.x);
            float angle = Mathf.Floor(r * 360 / (2 * Mathf.PI));
            arrow.transform.rotation = Quaternion.Euler(0, 90 - angle, 0);
        }
        else
        {
            Destroy(arrow);
        }


        if (target3.activeInHierarchy == true)
        {
            if (isCalledOnce == false)
            {
                isCalledOnce = true;
                Instantiate(arrow);
            }
            arrow.transform.position = player.transform.position + player.transform.forward * 1.0f + Vector3.up * 0.1f;
            Vector2 vec3 = new Vector2(target3.transform.position.x - player.transform.position.x,
                                       target3.transform.position.z - player.transform.position.z);
            float r = Mathf.Atan2(vec3.y, vec3.x);
            float angle = Mathf.Floor(r * 360 / (2 * Mathf.PI));
            arrow.transform.rotation = Quaternion.Euler(0, 90 - angle, 0);
        }
        else
        {
            Destroy(arrow);
        }

    }
    
}
