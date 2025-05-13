using UnityEngine;
using UnityEngine.UI;

public class RiddleScoreUI : MonoBehaviour
{
    [SerializeField] private Text scoreText; // Reference to the UI Text element

    void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("Score Text is not assigned! Drag the UI text object into the field.");
        }
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        if (RiddleManager.instance != null)
        {
            scoreText.text = "Riddle Pieces: " + RiddleManager.instance.ReturnScore() + "/3";
        }
    }
}