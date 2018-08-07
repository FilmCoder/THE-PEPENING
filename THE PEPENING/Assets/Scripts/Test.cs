using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GameObject[] array = new GameObject[1];
        array[1] = GameObject.Find("Spawner Plane");


        SpawnParams spawnParams
        = new SpawnParams(array);

        SpawnParams spawnParams2
        = new SpawnParams(spawnPlanes: new[] {GameObject.Find("Spawner Plane"), GameObject.Find("Spawner Plane")});

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
