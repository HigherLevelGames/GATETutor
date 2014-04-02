using UnityEngine;
using System.Collections;

[System.Serializable]
public class Step
{
    public string hint;
    public Vector3 position;

    public enum AnswerOption
    {
        And,
        Or,
        Not,
        Nand,
        Nor
    }
    public AnswerOption answer;
}