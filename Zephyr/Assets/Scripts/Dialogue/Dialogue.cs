using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Sentence
{
    public Speaker speaker;
    public int portraitIndex;

    [TextArea(3, 10)]
    public string sentText;
}

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public bool hasPortraits;
    //public Speaker speakerLeft;
    //public Speaker speakerRight;
    public Speaker[] speakerLeft;
    public Speaker[] speakerRight;
    public Sentence[] sentences;
}


