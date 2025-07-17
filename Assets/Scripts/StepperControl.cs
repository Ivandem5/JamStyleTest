using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StepperControl : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;    
    [SerializeField] private TMP_Text valueText;

    [Header("Stepper Configurations")]
    [SerializeField] private int minValue = 0;
    [SerializeField] private int maxValue = 10;
    [SerializeField] private int startValue = 10;

    public UnityEvent<int> onValueChanged;

    private int _currentValue;

    private void Awake()
    {        
        minValue = Mathf.Min(minValue, maxValue);
        startValue = Mathf.Clamp(startValue, minValue, maxValue);
        _currentValue = startValue;
    }

    private void OnEnable()
    {
        prevButton.onClick.AddListener(Decrease);
        nextButton.onClick.AddListener(Increase);
        RefreshUI();
    }

    private void OnDisable()
    {
        prevButton.onClick.RemoveListener(Decrease);
        nextButton.onClick.RemoveListener(Increase);
    }

    private void Increase()
    {
        if (_currentValue < maxValue)
        {
            _currentValue++;
            RefreshUI();
            onValueChanged?.Invoke(_currentValue);
        }
    }

    private void Decrease()
    {
        if (_currentValue > minValue)
        {
            _currentValue--;
            RefreshUI();
            onValueChanged?.Invoke(_currentValue);
        }
    }

    private void RefreshUI()
    {        
        valueText.text = $"{_currentValue}/{maxValue}";        
        prevButton.interactable = _currentValue > minValue;
        nextButton.interactable = _currentValue < maxValue;
    }
    
    public void SetLimits(int newMin, int newMax, bool resetToMin = true)
    {
        minValue = Mathf.Min(newMin, newMax);
        maxValue = Mathf.Max(newMin, newMax);
        _currentValue = resetToMin ? minValue : Mathf.Clamp(_currentValue, minValue, maxValue);
        RefreshUI();
    }
    
    public int GetValue() => _currentValue;
}
