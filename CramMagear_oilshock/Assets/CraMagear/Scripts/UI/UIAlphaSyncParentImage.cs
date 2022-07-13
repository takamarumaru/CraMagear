using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UniRx;
using UniRx.Triggers;

public class UIAlphaSyncParentImage : MonoBehaviour
{
    
    void Start()
    {
        Image parentImage = transform.parent.GetComponent<Image>();
        parentImage.ObserveEveryValueChanged(image => image.color.a).Subscribe(_ => 
        {
            Image thisImage = GetComponent<Image>();
            if (thisImage)
            {
                thisImage.color = new Color(thisImage.color.r, thisImage.color.g, thisImage.color.b, parentImage.color.a);
            }

            TMPro.TextMeshProUGUI thisText = GetComponent<TMPro.TextMeshProUGUI>();
            if (thisText)
            {
                thisText.color = new Color(thisText.color.r, thisText.color.g, thisText.color.b, parentImage.color.a);
            }

        }).AddTo(this);
    }
}
