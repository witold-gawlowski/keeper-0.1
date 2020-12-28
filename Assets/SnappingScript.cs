using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappingScript : MonoBehaviour
{
    List<GameObject> draggedLinkers;
    List<GameObject> otherLinkers;
    public float snappingDistance = 1.0f;

    float minDistance;
    GameObject aLinker, bLinker;
    private void FixedUpdate()
    {
        foreach(GameObject a in draggedLinkers)
        {
            foreach(GameObject b in otherLinkers)
            {
                Vector3 abVector = a.transform.position - b.transform.position;
                if(abVector.magnitude < minDistance)
                {
                    aLinker = a;
                    bLinker = b;
                    minDistance = abVector.magnitude;
                }
            }
        }
    }
    public void OnDragStart(BlockScript block)
    {
        draggedLinkers = new List<GameObject>();
        otherLinkers = new List<GameObject>();

        foreach(Transform t in block.transform)
        {
            if(t.CompareTag("Linker"))
            {
                draggedLinkers.Add(t.gameObject);
            }
        }

        List<BlockScript> otherBlocks = BlockManager.instance.Blocks;
        foreach(BlockScript b in otherBlocks)
        {
            foreach(Transform t in b.transform)
            {
                if (t.CompareTag("Linker"))
                {
                    otherLinkers.Add(t.gameObject);
                }
            }
        }
    }
}
