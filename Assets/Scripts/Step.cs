using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Step
{
    public string hint;
    public Vector3 position; // current position <-
    public List<Vector3> inputPositions = new List<Vector3>();

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