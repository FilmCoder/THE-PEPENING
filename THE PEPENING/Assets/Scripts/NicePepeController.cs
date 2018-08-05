using UnityEngine;

public class NicePepeController : MonoBehaviour {

    public float speed = 5f;
    GameObject player;

	// Use this for initialization
	void Start () {
        player = HelperFunctions.GetPlayerObject();
        HelperFunctions.PlaneLookAt(transform, player.transform);
	}

    void Update() {
        float distRemaining = Vector3.Distance(transform.position, player.transform.position);

        // have pepe seek the player, but don't get any closer than 2 units
        if (distRemaining > 2) {
            float steps = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position,
                                                     player.transform.position,
                                                     steps);
        }
	}
}
