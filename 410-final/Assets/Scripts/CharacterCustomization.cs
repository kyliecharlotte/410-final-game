using UnityEngine;

public class Character_Customization : MonoBehaviour
{

    public GameObject[] characters;
    public Transform playerStart;
    int selectedCharacter;
    private string selectedCharacterName = "SelectedCharacter";
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectedCharacter = PlayerPrefs.GetInt(selectedCharacterName, 0);
        player = Instantiate(characters[selectedCharacter], playerStart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
