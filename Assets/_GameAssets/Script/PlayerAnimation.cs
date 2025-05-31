using UnityEngine;
using UnityEngine.Video;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private Movement _movement;
    private StateController _playerState;

    void Awake()
    {
        _movement = GetComponent<Movement>();
        _playerState = GetComponent<StateController>();
    }

    void Start()
    {
        _movement.OnPlayerJumped += PlayerController_OnPlayerJumped;   
    }

    private void PlayerController_OnPlayerJumped()
    {
        _animator.SetBool("IsJumping", true);
        Invoke(nameof(ResetJumping), 0.5f);
    }

    private void ResetJumping()
    {
        _animator.SetBool("IsJumping", false);
    }

    // Update is called once per frame
    void Update()
    {
        SetPlayerAnimation();
    }

    private void SetPlayerAnimation()
    {
        var current = _playerState.GetPlayerState();

        switch (current)
        {
            case PlayerState.IDLE:
                _animator.SetBool("IsSliding", false);
                _animator.SetBool("IsMoving", false);
                break;
            case PlayerState.MOVE:
                _animator.SetBool("IsSliding", false);
                _animator.SetBool("IsMoving", true);
                break;
            case PlayerState.SLIDE:
               _animator.SetBool("IsSliding", true);
               _animator.SetBool("IsSlidingActive", false);
                break;
            case PlayerState.SLIDEIDLE:
               _animator.SetBool("IsSliding", false);
               _animator.SetBool("IsSlidingActive", true);
                break;      
        }
    }
}
