using UnityEngine;

public class EnemyHealthBarFollow : MonoBehaviour
{
    public Transform enemy; // Assign the enemy in the Inspector
    public Vector3 offset = new Vector3(0, 10, 0); // Adjust height

    void Update()
    {
        if (enemy == null) // Prevent accessing a destroyed object
        {
            Destroy(gameObject); // Destroy the health bar if the enemy is gone
            return;
        }

        transform.position = enemy.position + offset;
        transform.LookAt(Camera.main.transform);
    }

}
