using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBorn : MonoBehaviour
{
    // 生成するプレハブ格納用
    [Header("ランダム生成するキャラ")]
    [SerializeField] GameObject PrefabChara;

    [Header("ランダム出現座標：X")]
    [SerializeField] float minRandPos_x = 0;
    [SerializeField] float MaxRandPos_x = 0;

    [Header("ランダム出現座標：Y")]
    [SerializeField] float minRandPos_y = 0;
    [SerializeField] float MaxRandPos_y = 0;

    [Header("ランダム出現座標：Z")]
    [SerializeField] float minRandPos_z = 0;
    [SerializeField] float MaxRandPos_z = 0;

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
            float x = Random.Range(minRandPos_x, MaxRandPos_x);
            float y = Random.Range(minRandPos_y, MaxRandPos_y);
            float z = Random.Range(minRandPos_z, MaxRandPos_z);
            Vector3 pos = new Vector3(x, y, z);

            //時間リセット
            Timer = 0;

            // プレハブを生成
            Instantiate(PrefabChara, pos, Quaternion.identity);
        }
    }
}
