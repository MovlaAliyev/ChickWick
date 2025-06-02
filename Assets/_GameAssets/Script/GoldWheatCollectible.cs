using UnityEngine;

public class GoldWheatCollectible : MonoBehaviour, ICollectible
{

    [SerializeField] Movement _movement;
    [SerializeField] WheatDesignSO _wheatDesignSO;

    public void Collect()
    {
        _movement.SetMovementSpeed(_wheatDesignSO.IncreaseDecreaseSpeed, _wheatDesignSO.ExpireTime);
        Destroy(gameObject);
    }

}
