using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class Character_Customization : MonoBehaviour
{

    public GameObject[] characters;
    public Transform camera_ref;
    public Transform playerStart;
    int selectedCharacter;
    private string selectedCharacterName = "SelectedCharacter";
    public GameObject player;
    
    void Start()
    {
        // get chosen character value
        selectedCharacter = PlayerPrefs.GetInt(selectedCharacterName, 0);
        Debug.Log(selectedCharacter);
        Debug.Log(characters[selectedCharacter]);
        player = Instantiate(characters[selectedCharacter], playerStart);

        if (selectedCharacter == 0) {
            player.SetActive(true);
        }

        // Hard coding fixes for character selection

        if (selectedCharacter == 1) {
            player.transform.position = new Vector3(-0.3f, 1.23f, -3.8f);
        }
        // in case they look away

        player.transform.LookAt(camera_ref);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
