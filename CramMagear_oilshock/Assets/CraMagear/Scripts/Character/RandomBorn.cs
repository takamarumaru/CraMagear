using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBorn : MonoBehaviour
{
    // ��������v���n�u�i�[�p
    [Header("�����_����������L����")]
    [SerializeField] GameObject PrefabChara;

    [Header("�����_���o�����W�FX")]
    [SerializeField] float minRandPos_x = 0;
    [SerializeField] float MaxRandPos_x = 0;

    [Header("�����_���o�����W�FY")]
    [SerializeField] float minRandPos_y = 0;
    [SerializeField] float MaxRandPos_y = 0;

    [Header("�����_���o�����W�FZ")]
    [SerializeField] float minRandPos_z = 0;
    [SerializeField] float MaxRandPos_z = 0;

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
            float x = Random.Range(minRandPos_x, MaxRandPos_x);
            float y = Random.Range(minRandPos_y, MaxRandPos_y);
            float z = Random.Range(minRandPos_z, MaxRandPos_z);
            Vector3 pos = new Vector3(x, y, z);

            //���ԃ��Z�b�g
            Timer = 0;

            // �v���n�u�𐶐�
            Instantiate(PrefabChara, pos, Quaternion.identity);
        }
    }
}
