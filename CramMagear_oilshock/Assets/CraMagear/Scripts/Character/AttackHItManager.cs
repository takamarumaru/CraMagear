using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHItManager : MonoBehaviour
{
    MainObjectParameter _mainObjParam;

    //C++でいうstd::map
    // ヒットしたオブジェクトの、残り時間管理マップ
    Dictionary<MainObjectParameter, float> _hitObjects = new();

    // Start is called before the first frame update
    void Awake()
    {
        TryGetComponent(out _mainObjParam);
    }

    /// <summary>
    /// 登録リストに存在するか？
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool Exist(MainObjectParameter obj)
    {
        return _hitObjects.ContainsKey(obj);
    }

    /// <summary>
    /// 登録
    /// </summary>
    /// <param name="obj">登録するキャラクター</param>
    /// <param name="duration">持続時間</param>
    public void Register(MainObjectParameter obj, float duration)
    {
        _hitObjects.Add(obj, duration);
        //_hitObjects[obj] = duration;
    }

    /// <summary>
    /// 登録リストをすべてクリアする
    /// </summary>
    public void Reset()
    {
        _hitObjects.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        //Dictionaryのキーだけを、配列にコピーする
        List<MainObjectParameter> tempList = new();
        foreach (var key in _hitObjects.Keys)
        {
            tempList.Add(key);
        }

        //全内容を処理する
        foreach (var key in tempList)
        {
            float time = _hitObjects[key];
            time -= Time.deltaTime;
            _hitObjects[key] = time;

            //時間が尽きたものは、リストから削除
            if (time <= 0)
            {
                _hitObjects.Remove(key);
            }
        }
    }
}
