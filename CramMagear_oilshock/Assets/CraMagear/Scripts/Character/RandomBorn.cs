using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBorn : MonoBehaviour
{

    [Header("�G��ۊǂ���I�u�W�F�N�g")]
    [SerializeField] private Transform _enemyClone;

    // ��������v���n�u�i�[�p
    [Header("�����_����������L����")]
    [SerializeField] List<SpawnEnemyInfo> PrefabCharaList;

    [Header("�����_���o���͈�")]
    [SerializeField] float RandRange = 0;

    [Header("���b���ɏo�������邩")]
    [SerializeField] float BornTime = 0;
    //����
    float Timer = 0;

    [System.Serializable]
    struct SpawnEnemyInfo
    {
        public GameObject prefab;
        public float probability;
    }


    private void Awake()
    {
        Debug.Assert(PrefabCharaList != null, "RandomBorn�ɃL�����N�^�[���ݒ肳��Ă��܂���B");

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

            Vector3 pos = new Vector3(range + transform.position.x, transform.position.y, range + transform.position.z);

            //���ԃ��Z�b�g
            Timer = 0;

            //�m�����݂̂̃��X�g���쐬
            List<float> prefabIdxList = new List<float>();
            foreach (var info in PrefabCharaList)
            {
                prefabIdxList.Add(info.probability);
            }
            //�����_���Ŏ擾
            int randomIdx = RandomEX.GetIndexFromProbabilityList(prefabIdxList);
            // �v���n�u�𐶐�
            Instantiate(PrefabCharaList[randomIdx].prefab, pos, Quaternion.identity, _enemyClone);
        }
    }
}
