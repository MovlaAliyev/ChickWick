using UnityEngine;

public class GoldWheatCollectible : MonoBehaviour
{
    [SerializeField] float _increaseSpeed;
    [SerializeField] float _boostDuration;
    [SerializeField] Movement _movement;


    public void Collect()
    {
        _movement.SetMovementSpeed(_increaseSpeed, _boostDuration);
        Destroy(gameObject);
    }

}
