using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;

using DG.Tweening;

public class FadeInOutController : MonoBehaviour
{
    Image _blackCurtain;

    Tween _nowTween;

    void Awake()
    {
        TryGetComponent(out _blackCurtain);
    }

    public async UniTask FadeOut(float duration)
    {
        TweenExtensions.Kill(_nowTween);

        _blackCurtain.enabled = true;
        _nowTween = _blackCurtain.DOFade(0.0f,duration);
        await _nowTween;
        _blackCurtain.enabled = false;
    }

    public async UniTask FadeIn(float duration)
    {
        _blackCurtain.enabled = true;

        TweenExtensions.Kill(_nowTween);

        _nowTween = _blackCurtain.DOFade(1.0f, duration);
        await _nowTween;
        _blackCurtain.enabled = false;
    }
}
