using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerLookEnemy : MonoBehaviour
{
    [SerializeField] Transform Player;

    private GameObject playerObject;
    private Vector3 PlayerPosition; //プレイヤーの位置情報
    private Vector3 EnemyPosition;  //敵の位置情報

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");

        PlayerPosition = playerObject.transform.position;
        EnemyPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        //プレイヤーに向かってレイを飛ばす
        if(Physics.Raycast(transform.position, 
                           PlayerPosition - EnemyPosition,
                           out hit,
                           Mathf.Infinity))
        {
            if(hit.collider.tag == "Player")
            {

            }
            //PlayerPosition = playerObject.transform.position;
            //EnemyPosition = transform.position;

            //transform.position = EnemyPosition;
        }
    }
}
