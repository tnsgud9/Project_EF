using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Logic : MonoBehaviour
{
    public static IEnumerator WaitThenCallback(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }

    public static List<int> GetUniqueRandomNumbers(int min, int max, int count)
    {
        var uniqueNumbers = new HashSet<int>();

        if (max < count) count = max;
        // count 만큼 랜덤 숫자를 추출
        while (uniqueNumbers.Count < count)
        {
            var randomValue = Random.Range(min, max);
            uniqueNumbers.Add(randomValue); // HashSet에 추가 (중복값은 자동으로 처리됨)
        }

        // HashSet을 List로 변환하여 반환
        return new List<int>(uniqueNumbers);
    }
}