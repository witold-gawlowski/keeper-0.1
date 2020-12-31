using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public static BlockManager instance;
    List<BlockScript> livingBlocks;
    List<BlockScript> pool;
    public List<BlockScript> LivingBlocks
    {
        get { return livingBlocks; }
    }
    public void Spawn(Vector3 position)
    {
        if (pool.Count != 0)
        {
            BlockScript spawnedBlock = pool[0];
            pool.RemoveAt(0);
            livingBlocks.Add(spawnedBlock);
            spawnedBlock.transform.position = position;
        }
    }
    private void Awake()
    {
        instance = this;
        pool = new List<BlockScript>();
        livingBlocks = new List<BlockScript>();
        foreach (Inventory.Item item in Inventory.instance.contents)
        {
            for (int i = 0; i < item.count; i++)
            {
                GameObject newBlock = Instantiate(item.prefab, Vector3.one*100, Quaternion.identity);
                BlockScript newBlockScript = newBlock.GetComponent<BlockScript>();
                pool.Add(newBlockScript);
            }
        }
    }
}
