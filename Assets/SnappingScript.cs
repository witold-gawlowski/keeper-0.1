using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappingScript : MonoBehaviour
{
    public static SnappingScript instance;
    public float snappingDistance = 1.0f;

    float minDistance;
    GameObject linkerA, linkerB;
    public List<GameObject> draggedLinkers;
    public List<GameObject> otherLinkers;
    private void FixedUpdate()
    {
        if (DragScript.instance.DraggedBlock != null)
        {
            OnDrag();
        }
    }
    private void OnDrag()
    {
        minDistance = 1000;
        foreach (GameObject a in draggedLinkers)
        {
            foreach (GameObject b in otherLinkers)
            {
                if (Mathf.Abs(Mathf.Abs(a.transform.eulerAngles.z - b.transform.eulerAngles.z) - 180) < 5.0f)
                {
                    Vector3 abVector = a.transform.position - b.transform.position;
                    if (abVector.magnitude < minDistance)
                    {
                        linkerA = a;
                        linkerB = b;
                        minDistance = abVector.magnitude;
                    }
                }
            }
        }
        if (minDistance < snappingDistance)
        {
            DragScript.instance.DraggedBlock.TrySnap(linkerA, linkerB);
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

        foreach(Transform t in block.tilesParent.transform)
        {
            if(t.CompareTag("Linker"))
            {
                draggedLinkers.Add(t.gameObject);
            }
        }

        List<BlockScript> blocks = BlockManager.instance.LivingBlocks;
        foreach(BlockScript b in blocks)
        {
            if (b != null && b != block)
            {
                foreach (Transform t in b.tilesParent.transform)
                {
                    if (t.CompareTag("Linker"))
                    {
                        otherLinkers.Add(t.gameObject);
                    }
                }
            }
        }
    }
    private void Awake()
    {
        instance = this;
    }
}
