﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScript : MonoBehaviour
{
    public static DragScript instance;
    public float maxSpeed = 5;

    BlockScript draggedBlock;
    Vector3 gripShift;
    Vector3 shift = Vector3.up * 3.0f;
    Vector3 targetPos;
    Vector3 mousePos;
    float lastBlockPressedDownTime;

    public BlockScript DraggedBlock
    {
        get { return draggedBlock; }
    }
    void OnBlockPressedDown()
    {
        lastBlockPressedDownTime = Time.time;
        Collider2D col = Physics2D.OverlapPoint(mousePos, LayerMask.GetMask("Block"));
        draggedBlock = col.transform.GetComponentInParent<BlockScript>();
        gripShift = draggedBlock.transform.position - mousePos;

        SnappingScript.instance.OnDragStart(draggedBlock);
    }
    void OnBlockHeld()
    {
        targetPos = new Vector3(mousePos.x, mousePos.y, 0) + shift + gripShift;
    }
    void OnBlockReleased()
    {
        if (Time.time - lastBlockPressedDownTime < 0.2f)
        {
            draggedBlock.Rotate();
        }
        draggedBlock = null;
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
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
                OnBlockReleased();
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
    private void Awake()
    {
        instance = this;   
    }
}
