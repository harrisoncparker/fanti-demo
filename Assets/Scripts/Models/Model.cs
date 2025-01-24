using UnityEngine;

[System.Serializable]
public abstract class Model
{
    public string ToJson(bool prettyPrint = false)
    {
        return JsonUtility.ToJson(this, prettyPrint);
    }
}