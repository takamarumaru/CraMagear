using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchitectureCreator : MonoBehaviour
{
    [SerializeField] private Transform _architectureGuide;
    [SerializeField] private Transform _architecturePrefab;

    [SerializeField] private LayerMask _collisionLayer;

    [SerializeField] private Transform _center;
    [SerializeField] private float _architectureRange;

    [SerializeField] private MembersAdministrator _membersAdministrator;


    private Transform _guide;

    public bool _enable { get; private set; }

    private void Awake()
    {
        Debug.Assert(_architectureGuide != null, "ArchitectureCreatorにObjectが設定されていません。");
        Debug.Assert(_architecturePrefab != null, "ArchitectureCreatorにPrefabObjectが設定されていません。");
        Debug.Assert(_center != null, "ArchitectureCreatorにPlayerが設定されていません。");
        Debug.Assert(_membersAdministrator != null, "ArchitectureCreatorにMembersAdministratorが設定されていません。");
       
        _guide = Instantiate(_architectureGuide);
        _guide.gameObject.SetActive(_enable);
    }

    public void ShowGuide()
    {
        if (_enable == false) return;

        //画面の中心でレイ判定
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, Mathf.Infinity,_collisionLayer))
        {
            _guide.position = hit.point;
            
        }

        //当たった座標が範囲内ならガイドオブジェクトを表示
        if((hit.point-_center.position).magnitude > _architectureRange)
        {
            _guide.gameObject.SetActive(false);
        }
        else
        {
            _guide.gameObject.SetActive(true);
        }
    }

    public bool Create()
    {
        //ガイドオブジェクトが非アクティブなら実行しない
        if (_guide.gameObject.activeSelf == false) return false;

        //建築物を生成
        Transform transform = Instantiate(_architecturePrefab, _guide.position, _guide.rotation).transform;
        //memberを建築物の場所に派遣する
        if(_membersAdministrator.Dispatch(3,transform)==false)
        {
            Destroy(transform.gameObject);
        }

        return true;
    }

    public void EnableToggle()
    {
        _enable = !_enable;
        _guide.gameObject.SetActive(_enable);
    }
}
