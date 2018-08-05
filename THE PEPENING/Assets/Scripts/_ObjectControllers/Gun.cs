using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 200f;

    public Camera fpsCam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Fire1")) {
            Shoot();
        }
	}

    void Shoot() {
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) {
            
            Damageable target = hit.transform.GetComponent<Damageable>();
            if(target != null) {
                target.TakeDamage(damage);
            }
        }
    }
}
