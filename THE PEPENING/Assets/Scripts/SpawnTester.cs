using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The Spawner class is used to manage the spawning of enemies.
 */
public class SpawnTester : MonoBehaviour {

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
    private int pepeCount = 0;


    /******  PRIVATE  *********/


    //private Mesh mesh;
    //private Renderer rend;

    // Holds info on bounding boxs of spawnAreaPlanes, in World Coordinates.
    private Bounds[] spawnBoundsList;

    private float startSecond;
    private float stopSecond;

    // previous time an enemy was spawn
    private float prevSpawnTime = 0f;

	// Use this for initialization
	void Start () {
        //mesh = GetComponent<MeshFilter>().mesh;
        //rend = GetComponent<Renderer>();
        //Debug.Log(mesh);
        //Debug.Log("DONE!");

        init();
	}

    void init() {
        // fill out the bounds list for quick access to bounds info of each area plane.
        spawnBoundsList = new Bounds[spawnAreaPlanes.Length];
        for (int i = 0; i < spawnBoundsList.Length; i++) {
            spawnBoundsList[i] = spawnAreaPlanes[i].GetComponent<Renderer>().bounds;
        }

        // convert user friendly minute values to second values for internal consistency
        startSecond = startMinute / 60f;
        stopSecond = stopMinute / 60f;
    }

    // Update is called once per frame
    void Update() {
        if (ShouldSpawnEnemy()) {
            Debug.Log("spawning enemy");
            SpawnEnemy();
        }
    }

    private bool ShouldSpawnEnemy() {
        return Time.time - prevSpawnTime >= secondsBetweenSpawns;
    }

    private void SpawnEnemy() {
        prevSpawnTime = Time.time; // spawn happens now
        Vector3 spawnPoint = GetSpawnPoint(spawnBoundsList);
        Instantiate(enemyObject, spawnPoint, Quaternion.identity);
        pepeCount++;
        Debug.Log("count: " + pepeCount);
    }

    /*
     * Gets a random appropriate spawn point for an enemy, based on the 
     * areas specified by the spawnAreaPlanes
     */
    private static Vector3 GetSpawnPoint(Bounds[] boundsList)
    {
        System.Random rand = new System.Random();
        int i = rand.Next(boundsList.Length);
        return SpatialUtil.getPointInBounds(boundsList[i]);
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