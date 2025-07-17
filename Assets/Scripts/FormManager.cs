using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UI;

public class FormManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private StepperControl stepper;    
    [SerializeField] private Toggle checkboxToggle;    
    [SerializeField] private TMP_Dropdown listDropdown;    
    [SerializeField] private Slider sliderFull;    
    [SerializeField] private Slider sliderTicks;
    [SerializeField] private TMP_InputField nameInputField;

    [Header("Buttons")]
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button cancelButton;    

    [Header("Configurations")]
    [SerializeField] private float sliderTicksMultiplier;
    [SerializeField] private int stepperMinValue;
    [SerializeField] private int stepperMaxValue;
    [SerializeField] private int dropdownOptionsCount;

    [Header("External Events")]
    public UnityEvent<FormData> OnAccept;
    public UnityEvent OnCancel;

    private FormData _originalData;
    private FormData _currentData;    

    private void Awake()
    {
        AssertCheck();

        stepper.SetLimits(stepperMinValue, stepperMaxValue);
        PopulateDropdown();
        OnDataChanged();
        _originalData = _currentData;

        BindEvents();

        acceptButton.interactable = false;
    }


    private void AssertCheck()
    {
        Assert.IsNotNull(stepper, "Stepper is required");
        Assert.IsNotNull(checkboxToggle, "CheckboxToggle is required");
        Assert.IsNotNull(listDropdown, "ListDropdown is required");
        Assert.IsNotNull(sliderFull, "SliderFull is required");
        Assert.IsNotNull(sliderTicks, "SliderTicks is required");
        Assert.IsNotNull(nameInputField, "NameInputField is required");
        Assert.IsNotNull(acceptButton, "AcceptButton is required");
        Assert.IsNotNull(cancelButton, "CancelButton is required");        
    }

    private void PopulateDropdown()
    {
        var options = new List<string>(dropdownOptionsCount);
        for (int i = 0; i < dropdownOptionsCount; i++)
        {
            options.Add($"List selection {i + 1}");
        }
            
        listDropdown.ClearOptions();
        listDropdown.AddOptions(options);
    }

    private void BindEvents()
    {
        nameInputField.onValueChanged.AddListener(_ => OnDataChanged());
        stepper.onValueChanged.AddListener(_ => OnDataChanged());
        checkboxToggle.onValueChanged.AddListener(_ => OnDataChanged());
        listDropdown.onValueChanged.AddListener(_ => OnDataChanged());
        sliderFull.onValueChanged.AddListener(_ => OnDataChanged());
        sliderTicks.onValueChanged.AddListener(_ => OnDataChanged());

        acceptButton.onClick.AddListener(OnAcceptPressed);
        cancelButton.onClick.AddListener(OnCancelPressed);        
    }

    private void OnDataChanged()    {
        
        _currentData.Name = nameInputField.text;
        _currentData.Step = stepper.GetValue();
        _currentData.Checkbox = checkboxToggle.isOn;
        _currentData.Dropdown = listDropdown.value;
        _currentData.FullSlider = sliderFull.value;
        _currentData.TicksSlider = (int)(sliderTicks.value * sliderTicksMultiplier);
        
        acceptButton.interactable = !_currentData.Equals(_originalData);        
    }

    private void OnAcceptPressed()
    {        
        _originalData = _currentData;
        acceptButton.interactable = false;        
        OnAccept?.Invoke(_currentData);
    }

    private void OnCancelPressed()
    {        
        nameInputField.text = _originalData.Name;
        stepper.SetStep(_originalData.Step);
        checkboxToggle.isOn = _originalData.Checkbox;
        listDropdown.value = _originalData.Dropdown;
        sliderFull.value = _originalData.FullSlider;
        sliderTicks.value = _originalData.TicksSlider / sliderTicksMultiplier;
        
        acceptButton.interactable = false;
        OnAccept?.Invoke(_currentData);
    }
}

[System.Serializable]
public struct FormData
{
    public string Name;
    public int Step;
    public bool Checkbox;
    public int Dropdown;
    public float FullSlider;
    public int TicksSlider;

    public FormData(string name, int step, bool checkbox, int dropdown, float fullSlider, int ticksSlider)
    {
        Name = name;
        Step = step;
        Checkbox = checkbox;
        Dropdown = dropdown;
        FullSlider = fullSlider;
        TicksSlider = ticksSlider;
    }

    public bool Equals(FormData other)
    {
        return Name == other.Name
            && Step == other.Step
            && Checkbox == other.Checkbox
            && Dropdown == other.Dropdown
            && Mathf.Approximately(FullSlider, other.FullSlider)
            && Mathf.Approximately(TicksSlider, other.TicksSlider);
    }
}