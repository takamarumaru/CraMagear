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
        //�V���O���g��
        public static WinidowManager Instance { get; private set; }
        private void Awake()
        {
            if(Instance!=null)
            {
                //�폜�O��contents�̓��e�����ړ�������
                foreach (Transform content in _contants)
                {
                    content.SetParent(Instance._contants, false);
                }
                Destroy(gameObject);
                return;
            }
            Instance = this;

        }

        //�E�B���h�E�z�u�pGameObject��Transform
        [SerializeField] Transform _contants;

        [SerializeField] FadeInOutController _fadeInOut;
        public FadeInOutController FadeInOut => _fadeInOut;

        void Start()
        {
            //�q�̐����ύX���ꂽ��A���̓}�b�v��؂�ւ���
            this.ObserveEveryValueChanged(count => _contants.childCount).Subscribe(count => 
            {
                //�E�B���h�E���Ȃ��Ȃ�AGamePlay���[�h
                if(count ==0)
                {
                    PlayerInputManager.Instance.ChangeActionMap(PlayerInputManager.ActionMapTypes.GamePlay);
                }
                //�E�B���h�E������Ȃ�AUI���[�h
                else
                {
                    PlayerInputManager.Instance.ChangeActionMap(PlayerInputManager.ActionMapTypes.UI);
                }
            });

            _fadeInOut.FadeOut(1.0f).Forget();
        }

        //�E�B���h�E����
        public async UniTask<WindowType> CreateWindow<WindowType>(string name)
        {
            var windowAsset = await AssetBundleManager.Instance.LoadAssetAsync<GameObject>(name);
            //�E�B���h�E���̉�
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
