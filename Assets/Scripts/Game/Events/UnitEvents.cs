using UnityEngine;
using UnityEngine.Events;

public class UnitEvents
{
    public static UnityEvent<float, GameObject> OnTakeDamage;

    public static void InovokeTakingDamge(float damageAmount, GameObject damageSource)
    {
        OnTakeDamage.Invoke(damageAmount, damageSource);
    }
}
