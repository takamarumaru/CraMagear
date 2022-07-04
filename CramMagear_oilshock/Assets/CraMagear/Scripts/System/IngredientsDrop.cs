using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsDrop : MonoBehaviour
{
    [SerializeField] IngredientsManager _ingredientsManager;
    [SerializeField] int _dropNum;
    [SerializeField, Range(1.0f, 100.0f)] float _rareDropProbability;

    void AnimEvent_IngredientsDrop()
    {
        //�h���b�v����A�C�e���������_���Ō��߂�
        IngredientsManager.IngredientsInfo dropIngredients = new();

        for (int i = 0; i < _dropNum; i++)
        {
            if (Random.Range(1, 100) <= _rareDropProbability)
            {
                dropIngredients.electronicBoard++;
            }
            else 
            {
                dropIngredients.ancientGear++;
            }
        }

        //Debug.Log("newEnemyDown==================================");
        //Debug.Log(dropIngredients.electronicBoard);
        //Debug.Log(dropIngredients.ancientGear);

        //�f�ރ��X�g�ɒǉ�
        _ingredientsManager.AddIngredients(dropIngredients);
    }
}
