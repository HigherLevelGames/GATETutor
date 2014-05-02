using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Step
{
    public Vector2 Position2D;
    public Vector2 Scale;
    public enum AnswerOption
    {
        And,
        Or,
        Not,
        Nand,
        Nor
    }
    public AnswerOption answer;
    public string hint;
    public List<Line> inputLines = new List<Line>();
}

[System.Serializable]
public class Line
{
    public Vector2 begin;
    public Vector2 end;
}
