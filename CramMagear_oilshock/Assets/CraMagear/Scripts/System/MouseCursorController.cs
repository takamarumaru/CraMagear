using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorController : MonoBehaviour
{

    void Start()
    {

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
