using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name.ToString().Trim(), "Complete");
            PlayerPrefs.Save();
            Debug.Log("save");
            SceneManager.LoadScene("CharacterSelection");
        }
    }
}
