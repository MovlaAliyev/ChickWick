using UnityEngine;

public class PlayerIntegractionController : MonoBehaviour
{

    private bool hasTriggered = false;

    private Movement movement;

    void Awake()
    {
        movement = GetComponent<Movement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("here");
        if (hasTriggered) return;

        if (other.TryGetComponent(out ICollectible collectible))
        {
            hasTriggered = true;
            collectible.Collect();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ICollectible collectible))
        {
            hasTriggered = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out IBoostable boostable))
        {
           boostable.Boost(movement);
        }
    }

}
