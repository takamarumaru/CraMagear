using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProvider : MonoBehaviour
{
    //左軸取得
    public virtual Vector2 GetAxisL() => Vector2.zero;
    //右軸取得
    public virtual Vector2 GetAxisR() => Vector2.zero;

    //回転
    public virtual Quaternion GetRotation() => Quaternion.identity;

    //マウス移動量
    public virtual Vector2 GetMouse() => Vector2.zero;

    //攻撃ボタン
    public virtual bool GetButtonAttack() => false;

    //Aim切り替えボタン
    public virtual bool GetButtonAim() => false;
    public virtual bool GetButtonPressedAim() => false;

    //建築ボタン
    public virtual bool GetButtonArchitectureToggle() => false;

    //ジャンプボタン
    public virtual bool GetButtonJump() => false;

    //キャラクター回転
    public void RotateAxis(Vector3 forward,Transform trans)
    {

        //入力した方向に回転
        Quaternion rotation = Quaternion.LookRotation(forward, Vector3.up);

        //キャラクターの回転にlerpして回転
        trans.rotation = Quaternion.RotateTowards
             (
             trans.rotation,             //変化前の回転
             rotation,                   //変化後の回転
             720 * Time.deltaTime        //変化する角度
             );

    }
}
