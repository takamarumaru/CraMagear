using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField]
    GameObject _enemy;

    [SerializeField]
    GameObject _launchObj;

    [SerializeField]
    GameObject _launchEffect;

    private void Awake()
    {
        var parentObj = new GameObject("DropItem");
        parentObj.AddComponent<DestroyByChildCount>();

        var childObj = Instantiate(_launchObj);
        childObj.transform.position = _enemy.transform.position;
        childObj.transform.parent = parentObj.transform;
        childObj.SetActive(true);

        var childEffect = Instantiate(_launchEffect);
        childEffect.transform.parent = parentObj.transform;
        childEffect.SetActive(true);

        Destroy(gameObject);
    }
}
