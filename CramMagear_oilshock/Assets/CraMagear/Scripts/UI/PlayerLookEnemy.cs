using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerLookEnemy : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject player;
    [SerializeField] GameObject arrow;
    //[SerializeField] GameObject Rotate;  //ÉvÉåÉCÉÑÅ[ÇÃâÒì]
    Vector3 RotatePos;

    // Start is called before the first frame update
    void Start()
    {
        //var Scale = arrow.transform.DOScale(0.15f, 1f).SetLoops(-1, LoopType.Incremental);

        //var sequence1 = DOTween.Sequence(); //Sequenceê∂ê¨
        //Rotate = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        arrow.transform.position += player.transform.position - RotatePos;
        RotatePos = player.transform.position;

        transform.RotateAround(RotatePos, Vector3.up, 50.0f * Time.deltaTime);

        transform.position = player.transform.position + player.transform.forward * 1.2f + Vector3.up * 0.1f;

        Vector2 vec2 = new Vector2(target.transform.position.x - player.transform.position.x,
                           target.transform.position.z - player.transform.position.z);

        float r = Mathf.Atan2(vec2.y, vec2.x);
        float angle = Mathf.Floor(r * 360 / (2 * Mathf.PI));
        arrow.transform.rotation = Quaternion.Euler(0, 90 - angle, 0);

        //transform.DOScale(0.11f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        //arrow.transform.DOScale(0.15f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuart);
        
    }

}
