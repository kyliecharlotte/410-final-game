using UnityEngine;
using UnityEngine.Timeline;

public class Main_PlayerSelect : MonoBehaviour
{
    public GameObject player;
    int selectedCharacter;
    private string selectedCharacterName = "SelectedCharacter";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectedCharacter = PlayerPrefs.GetInt(selectedCharacterName, 0);
        //make sure tags of children match order in player dropdown
        player = player.transform.GetChild(selectedCharacter).gameObject;
        player.SetActive(true);
        //GameObject loadedPrefab = Resources.Load<GameObject>();

    }
    
    public GameObject returnPlayer () {
        return player.transform.GetChild(selectedCharacter).gameObject;
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
