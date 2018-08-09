using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class WaveManager : MonoBehaviour
{
    // attach spawn manager scripts to this object so they can run
    GameObject spawnManagerObject;

    // stores parameters for each wave, read in from waves.yaml
    List<WaveBlueprint> waves;
    int currentWaveNum = -1;

    // time when wave will cease (in seconds)
    float waveEndTime = 99999;

    // Use this for initialization
    void Start()
    {
        //spawnManager = gameObject.AddComponent<SpawnManager>();
        //spawnManager.PrintSwag();

        //SpawnParams sweg = new SpawnParams();
        //sweg.stopMinute = 55;
        //Debug.Log("sweg.stopMinute: " + sweg.stopMinute);

        spawnManagerObject = GameObject.Find("Spawn Manager");


        // parse waves.yaml and store result in "waves" variable
        var input = new StringReader(waveYAML);
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(new CamelCaseNamingConvention())
            .Build();  
        
        waves = deserializer.Deserialize<List<WaveBlueprint>>(input);

        // kick off the first wave
        currentWaveNum = 0;
        ExecuteWave(currentWaveNum);
    }

    // Update is called once per frame
    void Update() {
        if(Time.time > waveEndTime) {
            currentWaveNum++;
            ExecuteWave(currentWaveNum);
        }
    }

    public void ExecuteWave(int waveNum) {
        Debug.Log("Executing Wave: " + waveNum);
        WaveBlueprint wave = waves[waveNum];

        // used by Update() to start the next wave
        waveEndTime = Time.time + (wave.Minutes * 60.0f);

        // TODO: eventually display wave name, number, and set fog density
        Debug.Log("NEW WAVE NAME: " + wave.Name);
        Debug.Log("Fog: " + wave.FogDensity);

        // instantiate instances of spawnmanagers to handle spawning of enemy Pepes
        foreach (SpawnerBlueprint spawnerBlueprint in wave.Spawners)
        {
            // convert list of strings to array of GameObject spawner planes 
            int spawnPlaneCount = spawnerBlueprint.Planes.Count;
            GameObject[] spawnPlanes = new GameObject[spawnPlaneCount];
            for (int i = 0; i < spawnPlaneCount; i++)
            {
                spawnPlanes[i] = GameObject.Find(spawnerBlueprint.Planes[i]);
            }

            // instantiate a SpawnManager and attach it to a GameObject so it can start spawning
            SpawnManager manager = spawnManagerObject.AddComponent<SpawnManager>();
            manager.InitWithParams(new SpawnParams(
                spawnPlanes: spawnPlanes,
                enemyObject: (GameObject) Resources.Load("Prefabs/" + spawnerBlueprint.EnemyObject),
                startMinute: spawnerBlueprint.StartMinute,
                stopMinute: spawnerBlueprint.StopMinute,
                secondsBetweenSpawns: spawnerBlueprint.SecondsBetweenSpawns));
        }
    }

    public class WaveBlueprint {
        public string Name { get; set; }
        public float FogDensity { get; set; }
        public float Minutes { get; set; }
        public List<SpawnerBlueprint> Spawners { get; set; }
    }

    public class SpawnerBlueprint {
        public List<string> Planes { get; set; }
        public string EnemyObject { get; set; }
        public float StartMinute { get; set; }
        public float StopMinute { get; set; }
        public float SecondsBetweenSpawns { get; set; }
    }

    private const string waveYAML = @"
- name: hello wave
  fogDensity: 0.06
  minutes: 0.1
  spawners:
    - planes:
        - Spawner Plane
      enemyObject: Nice Pepe Prefab Root
      startMinute: 0
      stopMinute: 0.1
      secondsBetweenSpawns: 1

- name: SWAG WAVE
  fogDensity: 0.02
  minutes: 0.2
  spawners:
    - planes:
        - Spawner Plane
      enemyObject: Nice Pepe Prefab Root
      startMinute: 0
      stopMinute: 0.2
      secondsBetweenSpawns: 0.1
";
    
}