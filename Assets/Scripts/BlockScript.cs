using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public GameObject spritesObject;
    public GameObject tilesParent;
    public GameObject tilePrefab;
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
    private void SnapToGrid(Transform t)
    {
        Vector3 pos = t.localPosition;
        Vector3 newPos = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y));
        t.localPosition = newPos;
    }
    public void ResetTilePositions()
    {
        foreach(Transform t in tilesParent.transform)
        {
            SnapToGrid(t);
        }
    }
    public void Snap()
    {
        spritesObject.transform.parent = null;
        Vector3 childLinkerRelativePosition = (childLinker.transform.position - childLinker.transform.parent.position);
        spritesObject.transform.position = otherLinker.transform.position - childLinkerRelativePosition;
    }
    public void TrySnap(GameObject childLinker, GameObject otherLinker)
    {
        this.childLinker = childLinker;
        this.otherLinker = otherLinker;
        bool collidng = IsColliding(childLinker, otherLinker);
        if (!collidng)
        {
            Snap();
        }
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

        GameObject newTile = Instantiate(tilePrefab, childLinker.transform.position, Quaternion.identity);
        Destroy(childLinker);
        Destroy(otherLinker);
    }

    public bool IsColliding(GameObject childLinker, GameObject otherLinker)
    {
       Vector3 linkerDiff = otherLinker.transform.position - childLinker.transform.position;
       BlockScript otherBlock = otherLinker.GetComponentInParent<BlockScript>();       
       foreach(Transform childTile in tilesParent.transform)
       {
            foreach (Transform othersTile in otherLinker.transform.parent.transform)
            {
                bool blockers = childTile.tag == "Blocker" && othersTile.tag == "Blocker";
                bool linkers = childTile.tag == "Linker" && othersTile.tag == "Linker";
                if (!blockers && !linkers) {
                    Vector3 diff = childTile.transform.position - othersTile.transform.position + linkerDiff;
                    if (diff.magnitude < 0.1f)
                    {
                        return true;
                    }
                }
            }
       }
        return false;
    }

    void OnDrawGizmos()
    {
        foreach(Transform t in tilesParent.transform)
        {
            if (t.tag == "Blocker")
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(t.position, new Vector3(1, 1, 0));
            }
        }

    }
}
