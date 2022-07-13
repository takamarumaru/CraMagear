using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Cysharp.Threading;
using Cysharp.Threading.Tasks;

using DG.Tweening;

public class TabletItem_Test : MonoBehaviour
{
    [SerializeField] RectTransform _frameTransform;

    [SerializeField] Image _startUp;
    [SerializeField] float _startUpFadeTime;

    public async UniTask Initialize()
    {
        //起動時アイコン表示
        _startUp.color = new Color(_startUp.color.r, _startUp.color.g, _startUp.color.b,1.0f);
        await _startUp.DOFade(0.0f, _startUpFadeTime).SetEase(Ease.InQuint).SetUpdate(true);
    }

    public async UniTask Finalize()
    {
        await UniTask.Delay(1);
        Destroy(gameObject);
    }

    public async UniTask<bool> Execute()
    {
        var cancelToken = this.GetCancellationTokenOnDestroy();

        while (cancelToken.IsCancellationRequested == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                await Finalize();
            }
            //1フレーム待機
            await UniTask.DelayFrame(1);
        }

        return false;
    }
}
