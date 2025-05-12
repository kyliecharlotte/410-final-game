using UnityEngine;

public class Riddles : MonoBehaviour
{

    public int val = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            RiddleManager.instance.AddScore(val);
            Destroy(gameObject);
        }
    }
}
