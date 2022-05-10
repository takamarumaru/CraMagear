using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    [Tooltip("’e‚Ì‘¬‚³")]
    private float speed = 30f;

    // Start is called before the first frame update
    void Start()
    {

        //‘O•ûŒü
        Vector3 direction = transform.forward;
        this.gameObject.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        //íœ
        Destroy(this.gameObject, 2.0f);
    }
}
