using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;

using UniRx;
using UniRx.Triggers;

namespace WindowSystem
{
    public class WinidowManager : MonoBehaviour
    {
        //シングルトン
        public static WinidowManager Instance { get; private set; }
        private void Awake()
        {
            if(Instance!=null)
            {
                //削除前にcontentsの内容だけ移動させる
                foreach (Transform content in _contants)
                {
                    content.SetParent(Instance._contants, false);
                }
                Destroy(gameObject);
                return;
            }
            Instance = this;

        }

        //ウィンドウ配置用GameObjectのTransform
        [SerializeField] Transform _contants;

        [SerializeField] FadeInOutController _fadeInOut;
        public FadeInOutController FadeInOut => _fadeInOut;

        void Start()
        {
            //子の数が変更されたら、入力マップを切り替える
            this.ObserveEveryValueChanged(count => _contants.childCount).Subscribe(count => 
            {
                //ウィンドウがないなら、GamePlayモード
                if(count ==0)
                {
                    PlayerInputManager.Instance.ChangeActionMap(PlayerInputManager.ActionMapTypes.GamePlay);
                }
                //ウィンドウがあるなら、UIモード
                else
                {
                    PlayerInputManager.Instance.ChangeActionMap(PlayerInputManager.ActionMapTypes.UI);
                }
            });

            _fadeInOut.FadeOut(1.0f).Forget();
        }

        //ウィンドウ生成
        public async UniTask<WindowType> CreateWindow<WindowType>(string name)
        {
            var windowAsset = await AssetBundleManager.Instance.LoadAssetAsync<GameObject>(name);
            //ウィンドウ実体化
            var windowObj = Instantiate(windowAsset, _contants);

            var wnd = windowObj.GetComponent<WindowType>();

            return wnd;
        }

        public void DeleteAllWindow()
        {
            foreach(Transform window in _contants)
            {
                Destroy(window.gameObject);
            }
        }

        private async void Update()
        {
            if(PlayerInputManager.Instance.GamePlay_GetButtonMenu())
            {
                var wnd = await WindowSystem.WinidowManager.Instance.CreateWindow<Window_Menu>("MenuWindow");

                await wnd.Initialize();

                await wnd.Execute();
            }
        }
    }
}
