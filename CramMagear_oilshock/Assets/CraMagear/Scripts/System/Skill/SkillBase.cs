using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class SkillBase
{
    [Header("Settings")]
    public string Name;
    public bool isActive = true;
    abstract public void Invoke();
}
