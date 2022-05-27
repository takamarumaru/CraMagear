using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBorn : MonoBehaviour
{
    // 生成するプレハブ格納用
    [Header("ランダム生成するキャラ")]
    [SerializeField] GameObject PrefabChara;

    [Header("ランダム出現範囲")]
    [SerializeField] float RandRange = 0;

    [Header("何秒毎に出現させるか")]
    [SerializeField] float BornTime = 0;

    //時間
    float Timer = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //時間計測
        Timer += Time.deltaTime;

        // 設定した時間毎にシーンにプレハブを生成
        if (Timer >= BornTime)
        {
            // プレハブの位置をランダムで設定
            float range = Random.Range(-RandRange, RandRange);
            
            Vector3 pos = new Vector3(range+ transform.position.x, transform.position.y, range+ transform.position.z);

            //時間リセット
            Timer = 0;

            // プレハブを生成
            Instantiate(PrefabChara, pos, Quaternion.identity);
        }
    }
}
