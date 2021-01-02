using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public GameObject spritesObject;
    public GameObject tilesParent;
    private GameObject childLinker;
    private GameObject otherLinker;
    [SerializeField]
    private List<string> blockNames;
    public List<string> BlockNames
    {
        get { return blockNames; }
    }
    public GameObject ChildLinker;
    public GameObject OtherLinker;
    public bool IsSnapped()
    {
        return childLinker != null;
    }
    public void ResetTilePositions()
    {
        foreach(Transform t in tilesParent.transform)
        {
            Vector3 pos = t.localPosition;
            Vector3 newPos = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y));
            t.localPosition = newPos;
        }
    }
    public void Snap(GameObject childLinker, GameObject otherLinker)
    {
        this.childLinker = childLinker;
        this.otherLinker = otherLinker;
        spritesObject.transform.parent = null;
        Vector3 childLinkerRelativePosition = (childLinker.transform.position - childLinker.transform.parent.position);
        spritesObject.transform.position = otherLinker.transform.position - childLinkerRelativePosition;
    }
    public void UnSnap()
    {
        childLinker = null;
        otherLinker = null;
        spritesObject.transform.parent = this.transform;
        spritesObject.transform.localPosition = Vector3.zero;
    }
    public void Rotate()
    {
        transform.Rotate(Vector3.forward, 90);
    }
    public void Join()
    {
        BlockScript snappedBlock = otherLinker.GetComponentInParent<BlockScript>();
        transform.position = otherLinker.transform.position - (childLinker.transform.position - transform.position);
        spritesObject.transform.parent = transform;
        snappedBlock.spritesObject.transform.parent = spritesObject.transform;
        while (snappedBlock.tilesParent.transform.childCount != 0)
        {
            snappedBlock.tilesParent.transform.GetChild(0).parent = tilesParent.transform;
        }
        blockNames.AddRange(snappedBlock.BlockNames);
        Destroy(snappedBlock.gameObject);
    }
}
