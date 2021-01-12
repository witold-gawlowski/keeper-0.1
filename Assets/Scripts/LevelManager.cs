using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MyMath;
public class LevelManager : MonoBehaviour
{
    public class MapData
    {
        public List<string> blockTypes;
        public List<int> blockCounts;
        public GameObject mapPrefab;
        public int goal;
        public int limit;
    }
    public static LevelManager instance;
    public List<Vector2Int> mapCountDistribution;
    public List<Vector2Int> blockMultiplicityDistribution;
    public List<Vector2Int> blockTypeCountDistribution;
    public List<Vector2Int> goalDispersionDistribution;
    public List<Vector2Int> limitDistribution;
    public AnimationCurve levelGoal;
    public int LevelNumber { get { return level; } }
    private int level;
    private List<MapData> levels;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    private int GetRandomGoal()
    {
        float levelMean = levelGoal.Evaluate(Mathf.RoundToInt(level));
        return Mathf.RoundToInt(levelMean) + FromDistribution(goalDispersionDistribution);
    }

    private void CreateLevels()
    {
        int mapCount = FromDistribution(mapCountDistribution);
        levels = new List<MapData>();
        for (int i = 0; i < mapCount; i++)
        {
            MapData newMap = new MapData();
            newMap.goal = GetRandomGoal();
            newMap.limit = FromDistribution(limitDistribution);
            newMap.mapPrefab = MapDictionary.instance.GetRandomMap();
            newMap.blockTypes = new List<string>();
            newMap.blockCounts = new List<int>();
            int blockTypeCount = FromDistribution(blockTypeCountDistribution);
            for (int j=0; j<blockTypeCount; j++)
            {
                newMap.blockTypes.Add(BlockDictionary.instance.GetRandomBlock());
                newMap.blockCounts.Add(FromDistribution(blockMultiplicityDistribution));
            }
        }
    }

}
