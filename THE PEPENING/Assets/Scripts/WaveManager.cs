using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    //SpawnManager spawnManager;

    WaveLevelSettings[] waves;

    // Use this for initialization
    void Start()
    {
        //spawnManager = gameObject.AddComponent<SpawnManager>();
        //spawnManager.PrintSwag();

        SpawnParams sweg = new SpawnParams();
        sweg.stopMinute = 55;
        Debug.Log("sweg.stopMinute: " + sweg.stopMinute);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class WaveLevelSettings
{
    public float fogDensity = 0.04f; 
    public SpawnParams[] spawners;
}