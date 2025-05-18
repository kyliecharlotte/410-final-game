using UnityEngine;

public class Riddles : MonoBehaviour
{
    public int val = 1;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource found on Riddle GameObject!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RiddleManager.instance.AddScore(val);

            if (audioSource != null)
            {
                audioSource.Play();  // Play the pickup sound
                // Destroy the object after the clip finishes playing
                Destroy(gameObject, audioSource.clip.length);
            }
            else
            {
                Destroy(gameObject); // fallback if no AudioSource
            }

            if (RiddleManager.instance.ReturnScore() == 3)
            {
                RiddleManager.instance.TriggerRiddle();
            }
        }
    }
}
