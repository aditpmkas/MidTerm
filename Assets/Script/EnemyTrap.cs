using UnityEngine;

public class EnemyTrap : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Jika menyentuh Player
        {
            LifeSystem.instance.TakeDamage(); // Kurangi nyawa Player
        }
    }
}
