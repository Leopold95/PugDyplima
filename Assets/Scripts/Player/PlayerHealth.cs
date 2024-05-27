
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthBar _playerHealthBar;

    public void TakeDamage(float damage)
    {
        _playerHealthBar.TakeDamage(damage);
    }
}
