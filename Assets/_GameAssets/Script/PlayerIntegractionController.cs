using UnityEngine;

public class PlayerIntegractionController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("here");

        if (other.CompareTag("GoldWheat"))
        {
            other.gameObject?.GetComponent<GoldWheatCollectible>().Collect();
        }

        if (other.CompareTag("HolyWheat"))
        {
           other.gameObject?.GetComponent<HolyWheatCollectible>().Collect();
        }
        
        if (other.CompareTag("RottenWheat"))
        {
            other.gameObject?.GetComponent<RottenWheatCollectible>().Collect();
        }
        
    }
}
