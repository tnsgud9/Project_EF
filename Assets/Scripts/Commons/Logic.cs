using System;
using System.Collections;
using UnityEngine;

public class Logic : MonoBehaviour
{
    public static IEnumerator WaitThenCallback(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }
}