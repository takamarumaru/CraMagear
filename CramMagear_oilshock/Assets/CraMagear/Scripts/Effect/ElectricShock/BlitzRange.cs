using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlitzRange : MonoBehaviour
{

    private void Update()
    {
        //Destroy(gameObject, 5);
    }

    //Editer����̂ݎ��s�����AGizmo�`���p�֐�
    private void OnDrawGizmos()
    {
        float _radius = GetComponent<SphereCollider>().radius;

        Gizmos.color = new Color(1, 1, 0, 0.5f);

        //���C���[�t���[���̋���`��
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
