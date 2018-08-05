using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NicePepeMeshController : MonoBehaviour, Damageable {
    public float health = 1f;

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health < 0)
        {
            //Destroy(gameObject);
            Destroy(transform.parent.gameObject);
        }
    }
}
