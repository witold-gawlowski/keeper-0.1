﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDictionary : MonoBehaviour
{
    public static BlockDictionary instance;
    public List<BlockScript> contents;
    public string GetRandomBlock()
    {
        int index = Random.Range(0, contents.Count);
        return contents[index].GetName();
    }
    public Sprite GetBlockSprite(string name)
    {
        foreach(BlockScript bs in contents)
        {
            if (bs.GetName().Equals(name))
            {
                return bs.GetSprite();
            }
        }
        return null;
    }
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
