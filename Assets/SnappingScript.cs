using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappingScript : MonoBehaviour
{
    public static SnappingScript instance;
    public float snappingDistance = 1.0f;

    float minDistance;
    GameObject linkerA, linkerB;
    List<GameObject> draggedLinkers;
    List<GameObject> otherLinkers;
    private void FixedUpdate()
    {
        minDistance = 1000;
        foreach(GameObject a in draggedLinkers)
        {
            foreach(GameObject b in otherLinkers)
            {
                Vector3 abVector = a.transform.position - b.transform.position;
                if(abVector.magnitude < minDistance)
                {
                    linkerA = a;
                    linkerB = b;
                    minDistance = abVector.magnitude;
                }
            }
        }
        if (minDistance < snappingDistance)
        {
            DragScript.instance.DraggedBlock.Snap(linkerA, linkerB);
        }
        else
        {
            DragScript.instance.DraggedBlock.UnSnap();
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
    private void Awake()
    {
        instance = this;
    }
}
