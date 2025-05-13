// using UnityEngine;
// using System.Collections;
// using UnityEngine.UI;

// public class BarScript : MonoBehaviour
// {   
//     public float fillAmount;
//     [SerializeField]
//     private float lerpSpeed;
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     [SerializeField]
//     private Image content;
//     private Player_Stats playerStats;
//     public float MaxValue { get; set; }
//     // public Player_Stats script;
//     public float Value
//     {

//         set
//         {
//             fillAmount = Map(value, 0, MaxValue, 0, 1);
//         }
//     }
//     void Start()
//     {
//         fillAmount = 1.0f; // Set a test value

//         // Properly find Player_Stats once at the start
//         playerStats = FindObjectOfType<Player_Stats>(); 

//         if (playerStats != null)
//         {
//             playerStats.OnHealthChanged += UpdateHealthBar; // Subscribe once at the start
//         }
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         HandleBar();
//     }

//     private void HandleBar()
//     {
//         if (fillAmount != content.fillAmount)
//         {
//            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed); 
//         }
//     }

//     private float Map(float value, float inMin, float inMax, float outMin, float outMax) 
//     {
//         return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
//         // (80 - 0) * (1 - 0) / (100 - 0) + 0
//         // 80 * 1 / 100  = 0.8
//     }

//     private void UpdateHealthBar(int currentHealth)
//     {
//         if (MaxValue > 0)  // Prevent division by zero
//         {
//             Value = currentHealth; // This updates `fillAmount`
//         }
//     }

// }




// // using UnityEngine;
// // using UnityEngine.UI;

// // public class BarScript : MonoBehaviour
// // {
// //     public float fillAmount;
// //     [SerializeField]
// //     private float lerpSpeed;
// //     [SerializeField]
// //     private Image content;
    
// //     private Player_Stats playerStats;
// //     public float MaxValue { get; set; }

// //     public float Value
// //     {
// //         set
// //         {
// //             fillAmount = Map(value, 0, MaxValue, 0, 1);
// //         }
// //     }

// //     void Start()
// //     {


// //         playerStats = FindObjectOfType<Player_Stats>(); 

// //         if (playerStats != null)
// //         {
// //             playerStats.OnHealthChanged += UpdateHealthBar;
// //         }
// //         else
// //         {
// //             Debug.LogError("Player_Stats not found! Health bar cannot update.");
// //         }
// //     }

// //     void Update()
// //     {
// //         HandleBar();
// //     }

// //     private void HandleBar()
// //     {
// //         if (fillAmount != content.fillAmount)
// //         {
// //             content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
// //         }
// //     }

// //     private void UpdateHealthBar(int currentHealth)
// //     {
// //         if (MaxValue > 0)  // Prevent division by zero
// //         {
// //             Value = currentHealth; // This updates `fillAmount`
// //         }
// //     }

// //     private float Map(float value, float inMin, float inMax, float outMin, float outMax)
// //     {
// //         return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
// //     }
// // }




using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private float lerpSpeed = 5f;
    
    private float targetFill = 1f;

    void Start()
    {
        Debug.Log("HealthBarUI initialized. fillImage is " + (fillImage != null ? "set" : "null"));
    }


    public void SetMaxValue(float maxValue)
    {
        targetFill = 1f;
        fillImage.fillAmount = 1f;
    }

    public void UpdateValue(float currentValue, float maxValue)
    {
        targetFill = Mathf.Clamp01(currentValue / maxValue);
    }

    void Update()
    {
        if (fillImage.fillAmount != targetFill)
        {
            fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, targetFill, Time.deltaTime * lerpSpeed);
        }
    }
}



[System.Serializable]
public class Stat
{
    [SerializeField] private float maxVal;
    [SerializeField] private int currentVal;

    public Stat(int maxValue = 5)
    {
        MaxVal = maxValue;
        CurrentVal = maxValue;
    }

    public float MaxVal
    {
        get => maxVal;
        set => maxVal = value;
    }

    public float CurrentVal
    {
        get => currentVal;
        set => currentVal = Mathf.Clamp((int)value, 0, (int)MaxVal);
    }

    public void Initialize()
    {
        this.CurrentVal = currentVal;
    }
}

