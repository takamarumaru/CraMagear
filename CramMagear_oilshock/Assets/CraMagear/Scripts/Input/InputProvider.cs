using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProvider : MonoBehaviour
{
    //¶²æ“¾
    public virtual Vector2 GetAxisL() => Vector2.zero;
    //‰E²æ“¾
    public virtual Vector2 GetAxisR() => Vector2.zero;

    //UŒ‚ƒ{ƒ^ƒ“
    public virtual bool GetButtonAttack() => false;
}
