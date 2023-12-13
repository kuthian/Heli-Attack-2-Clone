using System;
using UnityEngine;

public abstract class ProgressBar : MonoBehaviour
{
    public float Percentage
    {
        get => percentFill.Value;
        set
        {
            percentFill.Value = value;
        }
    }

    [Range(0, 100)]
    [SerializeField]
    protected float _percentageValue;

    [SerializeField]
    protected RectTransform mask;
    [SerializeField]
    protected RectTransform fill;

    protected ObservableValue<float> percentFill = new ObservableValue<float>(100.0f);

    private void OnValidate()
    {
        Percentage = _percentageValue;
    }

    private void OnEnable()
    {
        percentFill.OnValueChanged += SetPercentFill;
    }

    private void OnDisable()
    {
        percentFill.OnValueChanged -= SetPercentFill;
    }

    private void Start()
    {

        Percentage = _percentageValue;
        SetPercentFill(_percentageValue);
    }

    abstract protected void SetPercentFill(float value);

}