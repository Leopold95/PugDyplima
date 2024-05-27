using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider _heathSlider;
    [SerializeField] Slider _easeSlider;
    [SerializeField] float _maxHeath = 100;
    [SerializeField] float _currentHealth;
    [SerializeField] float _lerpSpeed = 0.05f;

    private void Start()
    {
        SetMaxHealth(_maxHeath);
        _currentHealth = _maxHeath;
    }

    private void Update()
    {
        if(_heathSlider.value != _currentHealth)
        {
            _heathSlider.value = _currentHealth;
        }

        if (_heathSlider.value != _easeSlider.value)
        {
            _easeSlider.value = Mathf.Lerp(_easeSlider.value, _currentHealth, _lerpSpeed);
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
    }

    public void SetMaxHealth(float health)
    {
        _maxHeath = health;
        _heathSlider.maxValue = _maxHeath;
        _easeSlider.maxValue = _maxHeath;
        _currentHealth = _maxHeath;
    }
}
