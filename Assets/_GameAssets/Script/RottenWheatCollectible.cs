using UnityEngine;

public class RottenWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] float _decreaseSpeed;
    [SerializeField] float _boostDuration;
    [SerializeField] Movement _movement;


    public void Collect()
    {
        _movement.SetMovementSpeed(_decreaseSpeed, _boostDuration);
        Destroy(gameObject);
    }

}
