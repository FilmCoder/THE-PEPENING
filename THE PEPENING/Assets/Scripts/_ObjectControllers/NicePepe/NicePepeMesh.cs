using UnityEngine;

/*
 * Handles collision of Pepe with tendy bullets
 */
public class NicePepeMesh : MonoBehaviour, Damageable {
    public float health = 1f;

    public void TakeDamage(float amount) {
        health -= amount;

        if (health < 0) {
            // destroy parent pepe which holds all pepe-related objects
            Destroy(transform.parent.gameObject);
        }
    }
}
