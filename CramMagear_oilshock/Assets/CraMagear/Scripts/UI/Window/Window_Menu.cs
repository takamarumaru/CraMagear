using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Cysharp.Threading;
using Cysharp.Threading.Tasks;

using DG.Tweening;

public class Window_Menu : MonoBehaviour
{
    [SerializeField] RectTransform _frameTransform;

    [SerializeField] Image _menuGlow;

    [SerializeField] Image _screenBackGround;

    [SerializeField] AnimationCurve _slideEase;
    [SerializeField] float _slideSpeed = 1.0f;

    [SerializeField] AnimationCurve _glowEase;
    [SerializeField] float _glowTime = 1.0f;

    [SerializeField] float _screenFadeTime = 1.0f;

    [SerializeField] RectTransform _itemListParent;
    private List<RectTransform> _itemList = new();

    [SerializeField] RectTransform _SelectUI;
    private int _selectIdx = 0;

    public async UniTask Initialize()
    {
        //�w��A�C�e���̎q�����X�g�����
        for (int childIdx = 0; childIdx < _itemListParent.childCount; childIdx++)
        {
            RectTransform child = _itemListParent.GetChild(childIdx).GetComponent<RectTransform>();
            _itemList.Add(child);
        }

        _SelectUI.position = _itemList[_selectIdx].position;

        //��ʂ̉�������o�Ă���
        _frameTransform.localPosition = new Vector2(0,-_frameTransform.rect.height);
        await _frameTransform.DOLocalMove(Vector3.zero, _slideSpeed).SetEase(_slideEase).SetUpdate(true);

        //�N�������C�g�_��
        await _menuGlow.DOFade(1.0f, _glowTime).SetEase(_glowEase).SetUpdate(true);

        //�N������ʓ_��
        await _screenBackGround.DOFade(1.0f, _screenFadeTime).SetEase(Ease.Linear).SetUpdate(true);
    }

    public async UniTask Finalize()
    {
        //�I�������C�g�_��
        await _menuGlow.DOFade(1.0f, _glowTime/2).SetEase(_glowEase).SetUpdate(true);
        await _menuGlow.DOFade(1.0f, _glowTime/2).SetEase(_glowEase).SetUpdate(true);

        //�N������ʏ���
        await _screenBackGround.DOFade(0.0f, _screenFadeTime).SetEase(Ease.Linear).SetUpdate(true);

        //��ʂ̉����ɖ߂�
        _frameTransform.localPosition = Vector3.zero;
        await _frameTransform.DOLocalMove(new Vector2(0, -_frameTransform.rect.height), _slideSpeed).SetEase(_slideEase).SetUpdate(true);

        Destroy(gameObject);
    }

    public async UniTask<bool> Execute()
    {
        var cancelToken = this.GetCancellationTokenOnDestroy();

        while (cancelToken.IsCancellationRequested == false)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _selectIdx--;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _selectIdx++;
            }

            _selectIdx = Mathf.Clamp(_selectIdx, 0, _itemList.Count-1);

            _SelectUI.position = _itemList[_selectIdx].position;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (_selectIdx == 0)
                {
                    var wnd = await WindowSystem.WinidowManager.Instance.CreateWindow<TabletItem_Test>("TabletWindow_Test");

                    await wnd.Initialize();

                    //�E�B���h�E������܂ő҂�
                    var result = await wnd.Execute();
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                await Finalize();
            }

            //1�t���[���ҋ@
            await UniTask.DelayFrame(1);
        }

        return false;
    }
}
