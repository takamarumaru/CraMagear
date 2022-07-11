using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UniRx;
using UniRx.Triggers;

using DG.Tweening;

using Cysharp.Threading;
using Cysharp.Threading.Tasks;

public class Window_Test : MonoBehaviour
{

    [SerializeField] Button _buttonClose;

    [SerializeField] Button _buttonOK;

    [SerializeField] TMPro.TextMeshProUGUI _text;

    [SerializeField] RectTransform _frametTransform;

    bool _isOK = false;

    bool? _result = null;

    public async UniTask Initialize(string windowName)
    {
        gameObject.SetActive(false);

        _text.text = windowName;

        await UniTask.Delay(1000);

        gameObject.SetActive(true);

        //_button1.onClick.AddListener(() => { Debug.Log("ボタンを押した"); });

        _frametTransform.localScale = Vector3.zero;
        _frametTransform.DOScale(1.0f, 0.5f).SetEase(Ease.OutElastic).SetUpdate(true);

        //_frametTransform.position += new Vector3(Random.Range(-5,5), Random.Range(-5, 5),0);

        _buttonOK.OnClickAsObservable().Subscribe(_ => _isOK = true).AddTo(this);

        _buttonClose.OnClickAsObservable().Subscribe(_ => _result = true).AddTo(this);
        // thisが消滅したら、自動で購読を解除する
    }
    
    public async UniTask<bool> Execute()
    {
        var cancelToken = this.GetCancellationTokenOnDestroy();

        while(cancelToken.IsCancellationRequested == false)
        {
            Debug.Log("loop中");
            if (_isOK)
            {
                Debug.Log("OK");
                _isOK = false;

                var wnd = await WindowSystem.WinidowManager.Instance.CreateWindow<Window_Test>("TestWindow");

                await wnd.Initialize("Game Soft 4");
                
                //ウィンドウが閉じるまで待つ
                var result = await wnd.Execute();
            }

            if (_result.HasValue)
            {
                Debug.Log("CLOSE");
                Destroy(gameObject);

                return _result.Value;
            }
            //1フレーム待機
            await UniTask.DelayFrame(1);
        }

        return false;
    }
}
