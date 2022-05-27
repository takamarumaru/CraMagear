using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBorn : MonoBehaviour
{
    // ��������v���n�u�i�[�p
    [Header("�����_����������L����")]
    [SerializeField] GameObject PrefabChara;

    [Header("�����_���o���͈�")]
    [SerializeField] float RandRange = 0;

    [Header("���b���ɏo�������邩")]
    [SerializeField] float BornTime = 0;

    //����
    float Timer = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //���Ԍv��
        Timer += Time.deltaTime;

        // �ݒ肵�����Ԗ��ɃV�[���Ƀv���n�u�𐶐�
        if (Timer >= BornTime)
        {
            // �v���n�u�̈ʒu�������_���Őݒ�
            float range = Random.Range(-RandRange, RandRange);
            
            Vector3 pos = new Vector3(range+ transform.position.x, transform.position.y, range+ transform.position.z);

            //���ԃ��Z�b�g
            Timer = 0;

            // �v���n�u�𐶐�
            Instantiate(PrefabChara, pos, Quaternion.identity);
        }
    }
}
