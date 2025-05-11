using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{   
    // [SerializeField]
    public float fillAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private Image content;
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
        content.fillAmount = Map(56, 0, 0, 100, 1);
    }

    private float Map(float value, float inMin, float outMin, float inMax, float outMax) 
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        // (80 - 0) * (1 - 0) / (100 - 0) + 0
        // 80 * 1 / 100  = 0.8
    }

}
