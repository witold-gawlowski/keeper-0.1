using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MyMath
{
    public static int FromDistribution(List<Vector2Int> d)
    {
        float selector = Random.value;
        float sum = 0;
        int total = 0;
        for(int i = 0; i < d.Count; i++)
        {
            total += d[i].y;
        }

        for (int i = 0; i < d.Count; i++)
        {
            sum += d[i].y;
            if (selector < sum / total)
            {
                return d[i].x;
            }
        }
        return d[d.Count - 1].x;
    }   
}
