using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DOTweenTest : MonoBehaviour
{
    //private Image image;

    // Start is called before the first frame update
    void Start()
    {
        //image = GetComponent<Image>();

        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);

        //2秒待ってから(5,0,0)へ3秒で移動するのを4回(2往復) OutBounceで行う
        //this.transform.DOMove(new Vector3(5f, 0f, 0f), 3f)
        //                .SetDelay(2f)
        //                .SetLoops(4, LoopType.Yoyo)
        //                .SetEase(Ease.OutBounce);
    }

    // Update is called once per frame
    void Update()
    {
        // 左に移動
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    this.transform.Translate(-0.1f, 0.0f, 0.0f);
        //}
        //// 右に移動
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    this.transform.Translate(0.1f, 0.0f, 0.0f);
        //}
        //// 前に移動
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    this.transform.Translate(0.0f, 0.0f, 0.1f);
        //}
        //// 後ろに移動
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    this.transform.Translate(0.0f, 0.0f, -0.1f);
        //}        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    this.transform.Translate(-0.1f, 0.0f, 0.0f);
        //}
        //// 右に移動
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    this.transform.Translate(0.1f, 0.0f, 0.0f);
        //}
        //// 前に移動
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    this.transform.Translate(0.0f, 0.0f, 0.1f);
        //}
        //// 後ろに移動
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    this.transform.Translate(0.0f, 0.0f, -0.1f);
        //}
    }

    void OnClick()
    {
        transform.DOScale(1.1f, 0.5f).SetEase(Ease.OutElastic);
        GetComponentInChildren<Text>().text = "押された";
    }
}
