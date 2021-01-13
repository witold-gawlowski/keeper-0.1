using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDictionary : MonoBehaviour
{
    public static MapDictionary instance;
    public List<MapScript> contents;
    public MapScript GetRandomMap()
    {
        int levelNumber = contents.Count;
        int index = Random.Range(0, levelNumber);
        return contents[index];
    }
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
