using UnityEngine;

[System.Serializable]
public class RangeValue
{
    [SerializeField]
    private float crrValue;
    [SerializeField]
    private float maxValue;

    public float CurrentValue { get { return crrValue; } set { crrValue = Mathf.Clamp(value, 0, maxValue); } }
    public float MaxValue { get { return maxValue; } }
    public float Percentage { get { return crrValue / maxValue; } }
}