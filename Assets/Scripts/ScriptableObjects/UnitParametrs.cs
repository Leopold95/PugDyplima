using UnityEngine;

[CreateAssetMenu(fileName = "Tourret Paramas", menuName = "Units/New Unit Params")]
public class UnitParametrs : ScriptableObject
{
    public float MoveSpeed;
    public float Damage;
    public float Health;
    public float AttackDelay;
    public int ScoreCost;

    public AudioClip SoundAttack;
    public AudioClip SoundDeath;
    public AudioClip SoundGotDamage;
}
