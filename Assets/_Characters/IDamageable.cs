using UnityEngine;

public interface IDamageable {

    bool IsDestroyed();
    void TakeDamage(float damage, GameObject bullet);

}
