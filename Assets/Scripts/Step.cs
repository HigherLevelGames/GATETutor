using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Step
{
    public string hint;
    public Vector3 position; // current position <-
    //public List<Vector2> inputPositions = new List<Vector2>();
    public List<Line> inputLines = new List<Line>();

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

[System.Serializable]
public class Line
{
    public Vector2 begin;
    public Vector2 end;
}
