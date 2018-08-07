using UnityEngine;
using System.Reflection;

/**
 * Stores parameters for a SpawnManager object. 
 * 
 * Can be useful to store the parameters for multiple SpawnManager objects
 * for future instationation, like for example storing spawning information
 * for multiple levels/waves.
 */
public class SpawnParams : MonoBehaviour {
    
    /******    PUBLIC     ****/

    [Tooltip("Enemies will spawn anywhere within these planes.")]
    public GameObject[] spawnAreaPlanes;

    [Tooltip("Prefab specifing which type of enemy gameobject to spawn.")]
    public GameObject enemyObject;

    [Tooltip("Minute to start spawning enemies. (can be fraction values like 1.5)")]
    public float startMinute = 0;

    [Tooltip("Minute to stop spawning enemies. (can be fraction values like 2.5)")]
    public float stopMinute = 1;

    [Tooltip("An enemy will be created after this many seconds repeatedly.")]
    public float secondsBetweenSpawns = 5;

    public float enemySpeed;
    public int enemyHealth;


    public SpawnParams(GameObject[] spawnPlanes=null, GameObject enemyObject=null, float startMinute = 0,
                       float stopMinute = 1, float secondsBetweenSpawns = 5) {
        this.spawnAreaPlanes = spawnPlanes;
        this.enemyObject = enemyObject;
        this.startMinute = startMinute;
        this.stopMinute = stopMinute;
        this.secondsBetweenSpawns = secondsBetweenSpawns;
    }
}

/*
 * The Spawner class is used to manage the spawning of enemies.
 * 
 * Note on inheritence from SpawnParams: We separated the public variables
 * for SpawnParams into a separate class SpawnParams in order to store 
 * parameters for SpawnManager before instantiation. By making SpawnManager
 * a child of the parameter class, all those public variables will be present
 * as properties in SpawnManager, and therefore show up in the Unity IDE.
 */
public class SpawnManager : SpawnParams {

    // Holds info on bounding boxs of spawnAreaPlanes, in World Coordinates.
    Bounds[] spawnBoundsList;

    float startSecond;
    float stopSecond;

    // previous time an enemy was spawn
    float prevSpawnTime = 0f;


    /**************** PUBLIC METHODS ****************/

    /*
     * Initialize this SpawnManager.
     * 
     * If manually initializing a SpawnManager from a script, after setting
     * all parameters init() should be called.
     */
    public void Init() {
        // fill out the bounds list for quick access to bounds info of each area plane.
        spawnBoundsList = new Bounds[spawnAreaPlanes.Length];
        for (int i = 0; i < spawnBoundsList.Length; i++) {
            spawnBoundsList[i] = spawnAreaPlanes[i].GetComponent<Renderer>().bounds;
        }

        // convert user friendly minute values to second values for internal consistency
        startSecond = startMinute / 60f;
        stopSecond = stopMinute / 60f;
    }


    /*
     * Call InitWithParams to initialize spawn manager by passing in a 
     * parameter object with parameters predetermined.
     */
    public void InitWithParams(SpawnParams spawnParams) {

        // cycles through all properties in spawnParams and copies them 
        // to those same property fields in SpawnManager.
        foreach(PropertyInfo prop in GetType().GetProperties()) {
            object value = prop.GetValue(spawnParams, null);
            SetPropValue(this, prop.Name, value);
        }

        Init();
    }

    void SetPropValue(object obj, string propName, object value) {
        obj.GetType().GetProperty(propName).SetValue(this, value, null);
    }


    /**************** PRIVATE METHODS *****************/

    // Use this for initialization
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update() {
        if (ShouldSpawnEnemy()) {
            SpawnEnemy();
        }
    }

    bool ShouldSpawnEnemy() {
        return Time.time - prevSpawnTime >= secondsBetweenSpawns;
    }

    void SpawnEnemy() {
        prevSpawnTime = Time.time; // spawn happens now

        // create new enemy at spawn point
        Vector3 spawnPoint = GetSpawnPoint(spawnBoundsList);
        Instantiate(enemyObject, spawnPoint, Quaternion.identity);
    }

    /*
     * Gets a random appropriate spawn point for an enemy, based on the 
     * areas specified by the spawnAreaPlanes
     */
    static Vector3 GetSpawnPoint(Bounds[] boundsList)
    {
        System.Random rand = new System.Random();
        int i = rand.Next(boundsList.Length);
        return SpatialUtil.getPointInBounds(boundsList[i]);
    }

    public void PrintSwag() {
        Debug.Log("SpawnManager SWAG");
    }
}

/*
 * Utility spatial functions useful for spawning.
 */
public static class SpatialUtil {
    
    /*
     * Generate and return a random point within a UnityEngine.Bounds object, inclusive.
     */
    public static Vector3 getPointInBounds(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(x, y, z);
    }
}