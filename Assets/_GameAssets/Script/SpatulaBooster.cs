using UnityEngine;

public class SpatulaBooster : MonoBehaviour, IBoostable
{
    private bool isActivated;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _jumpForce;

    public void Boost(Movement movement)
    {

        if(isActivated) return;
        
        PlayBoostAnimation();

        Rigidbody rigidbody = movement.GetPlayerRigidbody();

        rigidbody.linearVelocity = new Vector3(rigidbody.linearVelocity.x, 0, rigidbody.linearVelocity.z);

        rigidbody.AddForce(transform.forward * _jumpForce, ForceMode.Impulse);
        isActivated = true;
        Invoke(nameof(ResetAnimation), 0.2f);
    }

    private void PlayBoostAnimation() => _animator.SetTrigger("IsSpatulaJumping");

    private void ResetAnimation()
    {
        isActivated = false;
    }
}
