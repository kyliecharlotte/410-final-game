using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthAmount = 20; 
    public float rotationSpeed = 50f; 

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime); 
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player_Stats playerStats = other.GetComponent<Player_Stats>();
            if (playerStats != null)
            {
                playerStats.Heal(healthAmount);
            }
            Destroy(gameObject); 
        }
    }
}
