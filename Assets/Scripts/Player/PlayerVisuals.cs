using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerVisuals : MonoBehaviour
{
    private Animator _animator;
    private float _animtationCrossfadeTime = 0.25f;
    private string _currentAnimation;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void UpdatePlayerVisuals(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Stay:
                SwitchAnimation("Idle");
                break;
            case PlayerState.Walk:
                SwitchAnimation("Walk");
                break;
            case PlayerState.Run:
                SwitchAnimation("Run");
                break;
            case PlayerState.Jump:
                //_animator.Play("Jump");
                SwitchAnimation("Jump");
                break;
            case PlayerState.MoveJump:
                break;
            case PlayerState.OnGround:
                break;
            case PlayerState.IntoAir:
                break;
        }
    }

    private void SwitchAnimation(string newAnimation)
    {
        if (_currentAnimation != newAnimation)
        {
            if (newAnimation == "Jump")
            {
                _animator.Play("Jump");
                return;
            }

            _currentAnimation = newAnimation;


            _animator.CrossFade(_currentAnimation, _animtationCrossfadeTime);
        }
    }
}
