using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public static BlockManager instance;
    List<BlockScript> blocks;
    public List<BlockScript> Blocks
    {
        get { return blocks; }
    }
    private void Awake()
    {
        instance = this;
        blocks = new List<BlockScript>(FindObjectsOfType<BlockScript>());
    }
}
