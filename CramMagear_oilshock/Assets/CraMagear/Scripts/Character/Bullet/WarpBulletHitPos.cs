using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpBulletHitPos : MonoBehaviour
{
    [SerializeField] GameObject _PlayWarpPos;
    [SerializeField] GameObject _EffectHole;

    Vector3 _HitPos = new Vector3(0.0f,0.0f,0.0f);

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("ŒÄ‚Î‚ê‚Ä‚é");
        //this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //gameObject.transform.position = _PlayWarpPos.transform.position;

    }

    private void OnCollisionEnter(Collision collision)
    {
        _HitPos = gameObject.transform.position + _PlayWarpPos.transform.position;

        //Debug.Log("Layer = " + collision.gameObject.layer);
        //Debug.Log("Layer2Name = " + LayerMask.LayerToName(collision.gameObject.layer));
        if (LayerMask.LayerToName(collision.gameObject.layer) == "StageMap")
        {

            _PlayWarpPos.transform.position = _HitPos;
            
            Debug.Log("PlayerPos = " + _PlayWarpPos.transform.position);
            //Instantiate(_EffectHole);
        }

    }
    

// Update is called once per frame
void Update()
    {

    }
}
