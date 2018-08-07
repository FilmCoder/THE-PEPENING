/*
 * Damageable classes can take damage from incoming tendy fire.
 * 
 * (So for example when a weapon attacks a Damageable class, the
 * weapon script will call the TakeDamage function of the attacked
 * object IF that object is Damageable.
 */
public interface Damageable {
    void TakeDamage(float amount);
}
