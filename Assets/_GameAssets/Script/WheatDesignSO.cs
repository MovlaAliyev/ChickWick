using UnityEngine;

[CreateAssetMenu(fileName = "WheatDesignSO", menuName = "WheatDesignSO", order = 0)]
public class WheatDesignSO : ScriptableObject
{
    [SerializeField] private float _expireTime;
    [SerializeField] private float _increaseDecreaseSpeed;


    public float ExpireTime => _expireTime;
    public float IncreaseDecreaseSpeed => _increaseDecreaseSpeed;

} 