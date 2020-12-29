using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public GameObject spritesObject;
    private BlockScript snappedBlock;
    public BlockScript SnappedBlock
    {
        get { return snappedBlock; }
    }
    public void ResetTilePositions()
    {
        foreach(Transform t in transform)
        {
            Vector3 pos = t.localPosition;
            Vector3 newPos = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y));
            t.localPosition = newPos;
        }
    }
    public void Snap(GameObject childLinker, GameObject otherLinker)
    {
        snappedBlock = otherLinker.GetComponentInParent<BlockScript>();
        spritesObject.transform.parent = null;
        Vector3 childLinkerRelativePosition = (childLinker.transform.position - childLinker.transform.parent.position);
        spritesObject.transform.position = otherLinker.transform.position - childLinkerRelativePosition;
    }
    public void UnSnap()
    {
        snappedBlock = null;
        spritesObject.transform.parent = this.transform;
        spritesObject.transform.localPosition = Vector3.zero;
    }
    public void Rotate()
    {
        transform.Rotate(Vector3.forward, 90);
    }
    public void Join()
    {
        foreach(Transform t in snappedBlock.transform)
        {
            t.parent = transform;
        }
        snappedBlock.spritesObject.transform.parent = spritesObject.transform;
        Destroy(SnappedBlock.gameObject);
    }
}
