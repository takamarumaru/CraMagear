using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEX : MonoBehaviour
{

    //確率のリストからインデックスを取得
    public static int GetIndexFromProbabilityList(List<float> probabilityList)
    {
        float maxprobability = 100.0f;

        for(int idx = 0 ; idx < probabilityList.Count ; idx++)
        {
            float value = Random.value * maxprobability;
            
            if (value <= probabilityList[idx])
            {
                return idx;
            }
            else
            {
                maxprobability -= probabilityList[idx];
            }
        }

        return 0;
    }
}
