using UnityEngine;

public class PlayerIntegractionController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("here");
        
        if (other.CompareTag("GoldWheat"))
        {
            Debug.Log("Gold entered the trigger");
        }

        if (other.CompareTag("HolyWheat"))
        {
            Debug.Log("Holy entered the trigger");
        }
        
        if (other.CompareTag("RottenWheat"))
        {
            Debug.Log("Rotten entered the trigger");
        }
        
    }
}
