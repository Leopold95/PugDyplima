using UnityEngine;

public class DamagableEntity : MonoBehaviour
{
    private float _health;
    [SerializeField] HealthBar _healthBar;

    //private List<GameObject> _collidingObjects;

    public GameObject LatDamageSource { get; private set; }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        _healthBar.TakeDamage(damage);

        if (_health <= 0)
            BeginDestroy();
    }

    public void TakeDamage(float damage, GameObject source)
    {
        TakeDamage(damage);
        LatDamageSource = source;
        //gameObject.SendMessage("OnTakeDamage", LatDamageSource, SendMessageOptions.RequireReceiver);
        gameObject.SendMessage("OnTakeDamage", LatDamageSource, SendMessageOptions.DontRequireReceiver);
    }

    public void SetMaxHealth(float value)
    {
        _healthBar.SetMaxHealth(value);
        _health = value;
    }

    private void BeginDestroy()
    {
        // Notify all colliding objects that this trigger object is being destroyed
        //foreach (var obj in _collidingObjects)
        //{
        //    if (obj != null) // Check if the object still exists
        //    {
        //        obj.SendMessage("OnDamagableEntityDestroyed", gameObject, SendMessageOptions.DontRequireReceiver);
        //    }
        //}

        // Notify dame dealler object is being destroyed
        //gameObject.SendMessage("OnShouldDestroy", LatDamageSource, SendMessageOptions.RequireReceiver);
        gameObject.SendMessage("OnShouldDestroy", LatDamageSource, SendMessageOptions.DontRequireReceiver);
    }
}
