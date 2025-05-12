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
        Cursor.visible = true;
        HideAllCharacters();
        selectedCharacter = 0;
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
        // set preference to store it across files
        PlayerPrefs.SetInt(selectedCharacterName, selectedCharacter);
        HideAllCharacters();
        SceneManager.LoadScene(mainScene);
    }
}
