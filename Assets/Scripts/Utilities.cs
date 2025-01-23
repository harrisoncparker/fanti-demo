using System;
using System.Collections;
using UnityEngine;

class Utilities
{
    public static IEnumerator WaitForAFrameThen(Action callback)
    {
        yield return new WaitForEndOfFrame();
        callback();
    }
}