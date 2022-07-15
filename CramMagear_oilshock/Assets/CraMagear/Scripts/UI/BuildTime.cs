using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BuildTime : MonoBehaviour
{
    [SerializeField] Image _UIImage;
    [SerializeField] CreateAfterSeconds _CreateAfterSeconds;
    private float _CountTime = 0.0f;
    private float _CreateTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _CreateTime = _CreateAfterSeconds.CreateTime;
        Debug.Log(_CreateTime);
        _UIImage.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        _UIImage.enabled = true;
        _CreateTime -= Time.deltaTime;
        Debug.Log(_CreateTime);
        _UIImage.DOFillAmount(1.0f, _CreateTime).SetEase(Ease.Linear).OnComplete(CompUI);
        //if (_CountTime > _CreateTime)
        //{
        //    _CountTime = 0.0f;
        //    if (_CountTime == 0.0f)
        //    {
        //        _UIImage.enabled = false;
        //    }
        //}
    }

    void CompUI()
    {
        _UIImage.enabled = false;
    }
}
