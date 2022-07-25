using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArchitectureCreator : MonoBehaviour
{
    //建築するオブジェクトの情報リスト
    [System.Serializable]
    public class CreateArchitecture
    {
        public Transform guide;
        public Transform product;
        public IngredientsManager.IngredientsInfo needIngredients;
    }
    [SerializeField] private List<CreateArchitecture> _createArchitectureList = new();
    private int _selectIdx = 0;

    //ガイドオブジェクト格納用
    private Transform _guide;

    //建築できるCollisionレイヤー
    [SerializeField] private LayerMask _collisionLayer;

    //建築可能範囲設定
    [SerializeField] private Transform _center;
    [SerializeField] private float _architectureRange;

    //隊員管理
    [SerializeField] private MembersAdministrator _membersAdministrator;
    //素材管理
    [SerializeField] private IngredientsManager _ingredientsManager;


    public bool _enable { get; private set; }

    private void Awake()
    {
        Debug.Assert(_createArchitectureList.Count != 0, "ArchitectureCreatorの_createArchitectureListが空です。");
        Debug.Assert(_center != null, "ArchitectureCreatorにPlayerが設定されていません。");
        Debug.Assert(_membersAdministrator != null, "ArchitectureCreatorにMembersAdministratorが設定されていません。");
       
        //最初のガイドオブジェクトを生成
        _guide = Instantiate(_createArchitectureList[_selectIdx].guide);
        _guide.gameObject.SetActive(_enable);
    }

    public void Update()
    {
        //デバッグ用の仮の処理、本来はPlayerのState等でSwitching()を呼び出す
        if (PlayerInputManager.Instance.GamePlay_GetListSwitchingLeft())
        {
            Switching(-1);
        }
        if (PlayerInputManager.Instance.GamePlay_GetListSwitchingRight())
        {
            Switching(1);
        }
    }

    public void Switching(int difference)
    {
        if (_enable == false) return;

        int newIdx = _selectIdx + difference;
        //範囲制限
        if (newIdx < 0) newIdx = _createArchitectureList.Count - 1;
        _selectIdx = newIdx % _createArchitectureList.Count;

        //ガイドオブジェクトの再生成
        Vector3 guidePostion = _guide.transform.position;
        DestroyImmediate(_guide.gameObject);
        _guide = Instantiate(_createArchitectureList[_selectIdx].guide, guidePostion, Quaternion.identity);
    }

    //public void AddArchitecture(Transform guide,Transform architecture)
    public void AddArchitecture(CreateArchitecture architecture)
    {
        _createArchitectureList.Add(architecture);
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

        if(_ingredientsManager.UseIngredients(_createArchitectureList[_selectIdx].needIngredients) == false)
        {
            return false;
        }

        //建築物を生成
        Transform transform = Instantiate(_createArchitectureList[_selectIdx].product, _guide.position, _guide.rotation).transform;
        //memberを建築物の場所に派遣する
        if(_membersAdministrator.Dispatch(0,transform)==false)
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
