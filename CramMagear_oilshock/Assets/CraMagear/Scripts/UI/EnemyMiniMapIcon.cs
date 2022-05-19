using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMiniMapIcon : MonoBehaviour
{
    Image EnemyIcon;
    private Sprite EnemyIconSp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.layer == 10)  //CharacterÉåÉCÉÑÅ[ÇÃéû
        {
            EnemyIconSp = Resources.Load<Sprite>("EnemyIcon");
            EnemyIcon = this.GetComponent<Image>();
            EnemyIcon.sprite = EnemyIconSp;
        }
    }
}
