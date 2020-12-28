using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public void ResetTilePositions()
    {
        foreach(Transform t in transform)
        {
            Vector3 pos = t.localPosition;
            Vector3 newPos = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y));
            t.localPosition = newPos;
        }
    }
}
