using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;

    // When health reaches 0 player dies
    public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
