using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlitzRange : MonoBehaviour
{
    //Editerからのみ実行される、Gizmo描画専用関数
    private void OnDrawGizmos()
    {
        float _radius = GetComponent<SphereCollider>().radius;

        Gizmos.color = new Color(1, 1, 0, 0.5f);

        //ワイヤーフレームの球を描画
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
