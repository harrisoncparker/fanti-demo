using System;
using System.Globalization;
using UnityEngine;

[Serializable]
public abstract class Model
{
    public string ToJson(bool prettyPrint = false)
    {
        return JsonUtility.ToJson(this, prettyPrint);
    }

    public DateTime DeserializeDateTime(string serializedDateTime)
    {
        return DateTime.TryParse(serializedDateTime, null, DateTimeStyles.RoundtripKind, out var parsed)
            ? parsed
            : DateTime.Now;
    }
}