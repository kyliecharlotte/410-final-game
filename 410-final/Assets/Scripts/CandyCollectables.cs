using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthAmount = 20; // Amount of health to restore
    public float rotationSpeed = 50f; // Speed of spinning

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime); // Spin the candy
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
            Destroy(gameObject); // Remove the collectable after pickup
        }
    }
}
