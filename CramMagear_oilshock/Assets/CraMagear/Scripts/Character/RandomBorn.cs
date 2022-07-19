using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBorn : MonoBehaviour
{

    [Header("敵を保管するオブジェクト")]
    [SerializeField] private Transform _enemyClone;

    // 生成するプレハブ格納用
    [Header("ランダム生成するキャラ")]
    [SerializeField] List<SpawnEnemyInfo> PrefabCharaList;

    [Header("ランダム出現範囲")]
    [SerializeField] float RandRange = 0;

    [Header("何秒毎に出現させるか")]
    [SerializeField] float BornTime = 0;
    //時間
    float Timer = 0;

    [System.Serializable]
    struct SpawnEnemyInfo
    {
        public GameObject prefab;
        public float probability;
    }


    private void Awake()
    {
        Debug.Assert(PrefabCharaList != null, "RandomBornにキャラクターが設定されていません。");

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

            Vector3 pos = new Vector3(range + transform.position.x, transform.position.y, range + transform.position.z);

            //時間リセット
            Timer = 0;

            //確率情報のみのリストを作成
            List<float> prefabIdxList = new List<float>();
            foreach (var info in PrefabCharaList)
            {
                prefabIdxList.Add(info.probability);
            }
            //ランダムで取得
            int randomIdx = RandomEX.GetIndexFromProbabilityList(prefabIdxList);
            // プレハブを生成
            Instantiate(PrefabCharaList[randomIdx].prefab, pos, Quaternion.identity, _enemyClone);
        }
    }
}
