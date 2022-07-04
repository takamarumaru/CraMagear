using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsManager : MonoBehaviour
{
    [System.Serializable]
    public struct IngredientsInfo
    {
        public int ancientGear;
        public int electronicBoard;
    }

    [SerializeField] IngredientsInfo _ingredientsPossession;

    public bool UseIngredients(IngredientsInfo ingredientsInfo)
    {
        if (ingredientsInfo.ancientGear > _ingredientsPossession.ancientGear ||
            ingredientsInfo.electronicBoard > _ingredientsPossession.electronicBoard)
        {
            Debug.Log("�f�ނ�����ĂȂ���");
            return false; 
        }

        //�f�ނ�����
        _ingredientsPossession.ancientGear -= ingredientsInfo.ancientGear;
        _ingredientsPossession.electronicBoard -= ingredientsInfo.electronicBoard;

        return true;
    }

    public void AddIngredients(IngredientsInfo ingredientsInfo)
    {
        //�f�ނ�ǉ�
        _ingredientsPossession.ancientGear += ingredientsInfo.ancientGear;
        _ingredientsPossession.electronicBoard += ingredientsInfo.electronicBoard;
    }

}
