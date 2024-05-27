using UnityEngine;

[CreateAssetMenu(fileName = "Tourret Paramas", menuName = "Tourrets/New Tourrent Params")]
public class TourretParametrs : ScriptableObject
{
    public int Damage;
    public float Health;
    public float AttackDelaySeconds;

    public AudioClip DeathSound;
    public AudioClip AttackhSound;
    public AudioClip SoundGotDamage;
}
