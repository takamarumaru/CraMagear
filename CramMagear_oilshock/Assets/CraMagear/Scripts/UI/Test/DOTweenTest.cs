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

        //2�b�҂��Ă���(5,0,0)��3�b�ňړ�����̂�4��(2����) OutBounce�ōs��
        //this.transform.DOMove(new Vector3(5f, 0f, 0f), 3f)
        //                .SetDelay(2f)
        //                .SetLoops(4, LoopType.Yoyo)
        //                .SetEase(Ease.OutBounce);
    }

    // Update is called once per frame
    void Update()
    {
        // ���Ɉړ�
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    this.transform.Translate(-0.1f, 0.0f, 0.0f);
        //}
        //// �E�Ɉړ�
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    this.transform.Translate(0.1f, 0.0f, 0.0f);
        //}
        //// �O�Ɉړ�
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    this.transform.Translate(0.0f, 0.0f, 0.1f);
        //}
        //// ���Ɉړ�
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    this.transform.Translate(0.0f, 0.0f, -0.1f);
        //}        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    this.transform.Translate(-0.1f, 0.0f, 0.0f);
        //}
        //// �E�Ɉړ�
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    this.transform.Translate(0.1f, 0.0f, 0.0f);
        //}
        //// �O�Ɉړ�
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    this.transform.Translate(0.0f, 0.0f, 0.1f);
        //}
        //// ���Ɉړ�
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    this.transform.Translate(0.0f, 0.0f, -0.1f);
        //}
    }

    void OnClick()
    {
        transform.DOScale(1.1f, 0.5f).SetEase(Ease.OutElastic);
        GetComponentInChildren<Text>().text = "�����ꂽ";
    }
}
