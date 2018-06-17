using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Record
{
    [SerializeField]
    public string name;
    [SerializeField]
    public int score;

    public Record(string name, int score)
    {
        this.name = name;
        this.score = score;
    }

    public string ToJson()
    {
        var str = "{\"name\":\"" + name + "\",\"score\":" + score.ToString() + "}";
        return str;
    }
}

[Serializable]
public class Records
{
    public List<Record> records;
}