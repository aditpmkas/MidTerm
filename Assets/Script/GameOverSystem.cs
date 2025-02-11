using UnityEngine;

public class GameOverSystem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LifeSystem.instance.TakeDamage();
        }
    }
}