using UnityEngine;

public class RiddleManager : MonoBehaviour
{

    public static RiddleManager instance;
    private int score;
    private RiddleScoreUI uiManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        uiManager = FindObjectOfType<RiddleScoreUI>(); 
        UpdateUI();

        if (instance == null) {
            instance = this;
        }
        score = 0;
    }

    // Update is called once per frame
    public void AddScore(int amt)
    {
        score += amt;
        UpdateUI();
    }

    public int ReturnScore() 
    {
        return score;
    }
    private void UpdateUI()
    {
        if (uiManager != null)
        {
            uiManager.UpdateScoreUI();
        }
    }
}
