using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    public GameObject[] characters;
    int selectedCharacter;
    private string selectedCharacterName = "SelectedCharacter";

    public AudioSource clickSoundSource;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        selectedCharacter = PlayerPrefs.GetInt(selectedCharacterName, 0);

    /*   
        if (clickSoundSource == null)
        {
            Debug.LogWarning("Click sound source not assigned in the Inspector!");
        }

        AddClickListenersToAllButtons();

    */
    }

    // Update is called once per frame

    public void Level_One()
    {
        //PlayClickSound();
        PlayerPrefs.SetInt(selectedCharacterName, selectedCharacter);
        SceneManager.LoadScene("castle");
    }

    public void Level_Two()
    {
        //PlayClickSound();
        if (PlayerPrefs.HasKey("castle"))
        {
            PlayerPrefs.SetInt(selectedCharacterName, selectedCharacter);
            SceneManager.LoadScene("castle 2");
        }
    }

    public void Level_Three()
    {
        //PlayClickSound();
        if (PlayerPrefs.HasKey("castle 2"))
        {
            PlayerPrefs.SetInt(selectedCharacterName, selectedCharacter);
            SceneManager.LoadScene("castle 3");
        }
    }

    /*
    private void PlayClickSound()
    {
        if (clickSoundSource != null)
        {
            clickSoundSource.Play();
        }
    }

    private void AddClickListenersToAllButtons()
    {
        var buttons = FindObjectsOfType<Button>();
        foreach (var button in buttons)
        {
            button.onClick.AddListener(PlayClickSound);
        }
    }
    */

    void Update()
    {
        
    }
}
