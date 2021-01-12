using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{ 
    [System.Serializable]
    public class Item
    {
        public GameObject prefab;
        public int count;
    }
    public static Inventory instance;
    public List<Item> contents;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public GameObject GetBlockPrefab(string name)
    {
        foreach(Item item in contents)
        {
            BlockScript blockScript = item.prefab.GetComponent<BlockScript>();
            if (blockScript.BlockNames[0].Equals(name))
            {
                return item.prefab;
            }
        }
        return null;
    }
}
