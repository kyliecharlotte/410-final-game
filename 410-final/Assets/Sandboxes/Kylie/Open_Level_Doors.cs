using GLTFast.Schema;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Open_Level_Doors : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Just for demo:
    // Check if enemy still alive

    public GameObject enemy;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8) {
            if (!enemy) {
                this.gameObject.SetActive(false);
                SceneManager.LoadScene("CharacterSelection");
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 8) {
            if (!enemy) {
                this.gameObject.SetActive(false);
                SceneManager.LoadScene("CharacterSelection");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
