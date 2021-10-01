using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.Animations;


[CreateAssetMenu(fileName = "New Speaker", menuName = "Speaker")]
public class Speaker : ScriptableObject
{
    public string name;
    public Sprite[] portraits;
    public RuntimeAnimatorController eyeController;
    public RuntimeAnimatorController mouthController;

}
