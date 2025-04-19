using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{

        public GameObject[] playerChoices;
        public int selectedCharacter;
        public string mainScene = "CharacterCustomization"; //TO BE CHANGED
        private string selectedCharacterName = "SelectedCharacter";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideAllCharacters();
        selectedCharacter = PlayerPrefs.GetInt(selectedCharacterName, 0);
        playerChoices[selectedCharacter].SetActive(true);

    }
    public void HideAllCharacters() {
        foreach (GameObject g in playerChoices) {
            g.SetActive(false);
        }
    }
    public void NextCharacter() {
        Debug.Log("clicked");
        playerChoices[selectedCharacter].SetActive(false);
        selectedCharacter++;
        if (selectedCharacter >= playerChoices.Length) {
            selectedCharacter = 0;
        }
        playerChoices[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter() {
        playerChoices[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0) {
            selectedCharacter = playerChoices.Length - 1;
        }
        playerChoices[selectedCharacter].SetActive(true);
    }

    public void StartGame() {
        PlayerPrefs.SetInt(selectedCharacterName, selectedCharacter);
        SceneManager.LoadScene(mainScene);
    }
}
