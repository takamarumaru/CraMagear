using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProvider : MonoBehaviour
{
    //左軸取得
    public virtual Vector2 GetAxisL() => Vector2.zero;
    //右軸取得
    public virtual Vector2 GetAxisR() => Vector2.zero;

    //攻撃ボタン
    public virtual bool GetButtonAttack() => false;

    //建築ボタン
    public virtual bool GetButtonArchitecture() => false;

    //ジャンプボタン
    public virtual bool GetButtonJump() => false;
}
