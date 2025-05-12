using UnityEngine;
using System;


[Serializable]
public class Stat 
{
    [SerializeField]
    private BarScript bar;
    [SerializeField]
    private float maxVal;
    [SerializeField]
    private int currentVal;
    

    public float CurrentVal
    {
        get
        {
            return currentVal;
        }
        set
        {
            this.currentVal = Mathf.Clamp((int)value, 0, (int)MaxVal);

            bar.Value = currentVal;
        }
    }

    public float MaxVal
    {
        get
        {
            return maxVal;
        }
        set
        {
            this.maxVal = value;
            bar.MaxValue = maxVal;
        }
    }

    public void Initialize() 
    {
        this.MaxVal = maxVal;
        this.CurrentVal = currentVal;
    }


}
