using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    [Tooltip("弾の速さ")]
    private float speed = 30f;

    // Start is called before the first frame update
    void Start()
    {

        //前方向
        Vector3 direction = transform.forward;
        this.gameObject.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        //削除
        Destroy(this.gameObject, 2.0f);
    }
}
