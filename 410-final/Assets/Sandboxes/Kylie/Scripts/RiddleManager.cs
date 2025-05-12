using UnityEngine;

public class RiddleManager : MonoBehaviour
{

    public static RiddleManager instance;
    private int score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        score = 0;
    }

    // Update is called once per frame
    public void AddScore(int amt)
    {
        score += amt;
    }

    public int ReturnScore() 
    {
        return score;
    }
}
