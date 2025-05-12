using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{   
    public float fillAmount;
    [SerializeField]
    private float lerpSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private Image content;
    public float MaxValue { get; set; }
    // public Player_Stats script;
    public float Value
    {
        set
        {
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }
    void Start()
    {
        fillAmount = 1.0f; // Set a test value
    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();
    }

    private void HandleBar()
    {
        if (fillAmount != content.fillAmount)
        {
           content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed); 
        }
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax) 
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        // (80 - 0) * (1 - 0) / (100 - 0) + 0
        // 80 * 1 / 100  = 0.8
    }

}
