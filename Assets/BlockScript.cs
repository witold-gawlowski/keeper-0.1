using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public GameObject spritesObject;
    public void ResetTilePositions()
    {
        foreach(Transform t in transform)
        {
            Vector3 pos = t.localPosition;
            Vector3 newPos = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y));
            t.localPosition = newPos;
        }
    }
    public void Snap(GameObject linkerA, GameObject linkerB)
    {
        spritesObject.transform.parent = null;
        spritesObject.transform.position = linkerB.transform.position - (linkerA.transform.position-linkerA.transform.parent.position);
    }
    public void UnSnap()
    {
        spritesObject.transform.parent = this.transform;
        spritesObject.transform.localPosition = Vector3.zero;
    }
    public void Rotate()
    {
        transform.Rotate(Vector3.forward, 90);
    }
}
