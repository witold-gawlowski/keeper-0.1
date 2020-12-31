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
}
