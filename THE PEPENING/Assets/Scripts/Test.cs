using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using YamlDotNet.RepresentationModel;
using YamlDotNet.Samples.Helpers;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;


public class Test : MonoBehaviour {



	// Use this for initialization
	void Start () {

        //SpawnParams spawnParams
        //= new SpawnParams(array);

        //SpawnParams spawnParams2
        //= new SpawnParams(spawnPlanes: new[] {GameObject.Find("Spawner Plane"), GameObject.Find("Spawner Plane")});

        TestSpawnerCreation();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void TestSpawnerCreation() {
        GameObject spawnManagerObject = GameObject.Find("Spawn Manager");
        GameObject[] spawnerPlanes = { GameObject.Find("Spawner Plane") };
        GameObject pepe = (GameObject) Resources.Load("Prefabs/Nice Pepe Prefab Root");

        SpawnParams spawnParams = new SpawnParams(spawnerPlanes, pepe, 0, 0.1f, 0.1f);
        SpawnManager spawnManager = spawnManagerObject.AddComponent<SpawnManager>();
        spawnManager.InitWithParams(spawnParams);
    }

    void TestReadingYAML()
    {
        //// Setup the input
        //var input = new StringReader(waveYAML);

        //// Load the stream
        //var yaml = new YamlStream();
        //yaml.Load(input);

        //// Examine the stream
        //var waves =
        //    (YamlMappingNode)yaml.Documents[0].RootNode;

        //foreach (var wave in waves.Children)
        //{

        //    Debug.Log(((YamlScalarNode)wave.Key).Value);

        //    // read in wave object, initialize wave settings
        //    // and instantiate spawners

        //}

        ////// List all the items
        ////var items = (YamlSequenceNode)waves.Children[new YamlScalarNode("items")];
        ////foreach (YamlMappingNode item in items)
        ////{
        ////    Debug.Log(
        ////        "{0}\t{1}" + 
        ////        item.Children[new YamlScalarNode("part_no")] + 
        ////        item.Children[new YamlScalarNode("descrip")]
        ////    );
        ////}
        /// 
        /// 
        ///


        // Deserialization process, read in the YAML which specifies the
        // parameters for the waves, and store that info into a C# object






        var input = new StringReader(waveYAML);

        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(new CamelCaseNamingConvention())
            .Build();

        var waves = deserializer.Deserialize<List<WaveBlueprint>>(input);

        Debug.Log("Done deserialization of WAVES.... !!");


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
  minutes: 1
  spawners:
    - planes:
        - Spawner Plane
      enemyObject: NicePepePrefab
      startMinute: 0
      stopMinute: 1
      secondsBetweenSpawns: 2

- name: SWAG WAVE
  fogDensity: 0.02
  minutes: 2
  spawners:
    - planes:
        - Spawner Plane
      enemyObject: NicePepePrefab
      startMinute: 3
      stopMinute: 4
      secondsBetweenSpawns: 2
";

}
