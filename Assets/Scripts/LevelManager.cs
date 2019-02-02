using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    //Stores the possible levels that can be spawned
    public List<GameObject> levels;

    //The level that the player always starts on
    public GameObject startingLevel;
    public GameObject endingLevel;

    //Material for sprite that lets lighting occur
    public Material spriteWithLighting;

    //The prefab where everything in the level goes
    public Transform levelPrefab;

    public static LevelManager Instance;

    public PlayerData pd = PlayerData.Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }

    private void Start()
    {
        GenerateLevels(PlayerData.Instance.levelCount);
    }

    /// <summary>
    /// Stores the transform of the player
    /// </summary>
    public Transform player;

    public void GenerateLevels(int levelCount)
    {
        //Instantiates the first level
        GameObject lastLevel = Instantiate(startingLevel, new Vector2(0, 0), Quaternion.identity, levelPrefab);

        for (int i = 0; i < levelCount; i++)
        {
            //ADD CHECK THAT THE SAME LEVEL WASN'T PUT IN TWICE

            //The level that's going to be instantiated
            GameObject newLevel = levels[Random.Range(0, levels.Count)];
            //The level component on the new level
            Level newLevelComponent = newLevel.GetComponent<Level>();
            //The ending point of the last level
            Vector3 lastEndingPoint = lastLevel.GetComponent<Level>().endingPoint.position;
            //The new position for the level to be instantiated at
            Vector3 newLevelPosition = GetLevelInstantiationPoint(lastEndingPoint, newLevelComponent);

            //Instantiates a new random level, than sets the last level to be the new level
            lastLevel = Instantiate(newLevel,  newLevelPosition, Quaternion.identity, levelPrefab);
        }

        //Instantiates the last level at the correct position (this line is a little messy)
        Instantiate(endingLevel, GetLevelInstantiationPoint(lastLevel.GetComponent<Level>().endingPoint.position, endingLevel.GetComponent<Level>()), Quaternion.identity, levelPrefab);

        if (PlayerData.Instance.lowLighting)
        {
            SetLightingMaterialRecursively(spriteWithLighting, levelPrefab);
        }
        
    }

    /// <summary>
    /// Sets all objects material through recursion
    /// </summary>
    void SetLightingMaterialRecursively(Material material, Transform obj)
    {
        for (int i = 0; i < obj.childCount; i++)
        {
            Transform currentChild = obj.GetChild(i);

            SpriteRenderer rend = currentChild.GetComponent<SpriteRenderer>();

            if(rend != null)
            {
                rend.material = material;
            }

            if (currentChild.childCount == 0)
                continue;

            SetLightingMaterialRecursively(material, currentChild);

        }
    }

    /// <summary>
    /// Gets the point to Instantiate a new level given the last level's
    /// ending point and the new level
    /// </summary>
    /// <param name="lastLevelEndPoint"></param>
    /// <param name="newLevel"></param>
    /// <returns></returns>
    private Vector3 GetLevelInstantiationPoint(Vector3 lastLevelEndPoint, Level newLevel)
    {
        Vector3 returnVector = lastLevelEndPoint - newLevel.startingPoint.localPosition;
        returnVector.y = 0;
        returnVector.x -= 0.1f;

        return returnVector;
    }

}
