using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScript : MonoBehaviour
{
    BlockScript draggedBlock;
    Vector3 gripShift;
    Vector3 shift = Vector3.up * 3.0f;
    Vector3 targetPos;
    Vector3 mousePos;
    public float maxSpeed = 5;
    void OnBlockPressedDown()
    {
        Collider2D col = Physics2D.OverlapPoint(mousePos);
        draggedBlock = col.transform.GetComponentInParent<BlockScript>();
        gripShift = draggedBlock.transform.position - mousePos;
    }
    void OnBlockHeld()
    {
        targetPos = new Vector3(mousePos.x, mousePos.y, 0) + shift + gripShift;
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                OnBlockPressedDown();
            }
            if (draggedBlock)
            {
                OnBlockHeld();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (draggedBlock)
            {
                draggedBlock = null;
            }
        }
    }
    void FixedUpdate()
    {
        if (draggedBlock)
        {
            Vector3 positionDelta = targetPos - draggedBlock.transform.position;
            if(positionDelta.magnitude > maxSpeed * Time.fixedDeltaTime)
            {
                positionDelta = maxSpeed * Time.fixedDeltaTime * positionDelta.normalized;
            }
            draggedBlock.transform.position += positionDelta;
        }
    }
}
