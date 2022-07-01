using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSearcher : MonoBehaviour
{
    //検索対象のレイヤーマスク
    [SerializeField] LayerMask _layerMasks;

    [SerializeField] MainObjectParameter _mainObjectParam;

    [SerializeField] float _radus;

    //検索対象
    [System.Flags]
    public enum ComparisonFlags
    {
        None        =   0,
        SameTeam    =   1 << 0,
        OtherTeam   =   1 << 1,
    }
    [SerializeField] ComparisonFlags _comparisonFlags;


    //ターゲット情報
    public struct TargetNode
    {
        public float distance;                              //相手との距離
        public MainObjectParameter MainObjectParameter;     //相手のパラメータ
    }
    List<TargetNode> _targets = new();

    //作業用配列
    int _numColliders = 0;
    Collider[] _tempColliders = new Collider[100];

    public TargetNode? GetClosestTarget()
    {
        if (_targets.Count == 0) return null;
        return _targets[0];
    }


    private void Update()
    {
        //ターゲットのリストを削除
        _targets.Clear();

        //判定実行
        //Collider[] colliders = Physics.OverlapSphere(transform.position, _radus, _layerMasks);
        _numColliders = Physics.OverlapSphereNonAlloc(transform.position, _radus,_tempColliders, _layerMasks);

        Debug.Log(_numColliders);
        //foreach(Collider collider in colliders)
        for (int i=0;i<_numColliders;i++)
        {
            Collider  collider = _tempColliders[i];
            //自分を無視
            var targetParam = collider.attachedRigidbody.GetComponent<MainObjectParameter>();
            if(_mainObjectParam == targetParam)
            {
                continue;
            }
            //敵味方判定
            bool teamResult = false;
            if(_comparisonFlags.HasFlag(ComparisonFlags.SameTeam))
            {
                teamResult |= (_mainObjectParam.TeamID == targetParam.TeamID);
            }
            if (_comparisonFlags.HasFlag(ComparisonFlags.OtherTeam))
            {
                teamResult |= (_mainObjectParam.TeamID != targetParam.TeamID);
            }
            if (teamResult == false) continue;


            TargetNode node = new();
            node.MainObjectParameter = targetParam;
            node.distance = (collider.transform.position - transform.position).magnitude;
            _targets.Add(node);
        }

        //ソート
        _targets.Sort((a, b) => a.distance.CompareTo(b.distance));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _radus);
    }
}
