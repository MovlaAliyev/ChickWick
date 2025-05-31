using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] float _forcceJump;
    [SerializeField] float _boostDuration;
    [SerializeField] Movement _movement;


    public void Collect()
    {
        _movement.SetMovementSpeed(_forcceJump, _boostDuration);
        Destroy(gameObject);
    }

}
