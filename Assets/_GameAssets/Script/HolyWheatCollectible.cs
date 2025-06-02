using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] Movement _movement;
    [SerializeField] WheatDesignSO _wheatDesignSO;

    public void Collect()
    {
        _movement.SetJumpForce(_wheatDesignSO.IncreaseDecreaseSpeed, _wheatDesignSO.ExpireTime);
        Destroy(gameObject);
    }

}
