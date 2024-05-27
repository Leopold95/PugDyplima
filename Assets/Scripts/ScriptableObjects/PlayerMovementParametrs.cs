using UnityEngine;

[CreateAssetMenu(fileName = "Player Movement Paramas", menuName = "Player/New Movement Player Params")]
public class PlayerMovementParametrs : ScriptableObject
{
    public float MoveSpeed;
    public float RunSpeed;
    public float JumpHeight;
    public float GravityValue;
    public float RotationSpeed;
}
