using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(TMP_Dropdown))]
public class DropdownArrowController : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private RectTransform arrow;

    private Quaternion _downRotation = Quaternion.Euler(0, 0, 0f);
    private Quaternion _upRotation = Quaternion.Euler(0, 0, 180f);
   
    private bool _previousExpanded;

    private void Awake()
    {
        if (dropdown == null) 
            dropdown = GetComponent<TMP_Dropdown>();
        Assert.IsNotNull(arrow, "Arrow is not found!");

        dropdown.onValueChanged.AddListener(_ => arrow.localRotation = _downRotation);        
        _previousExpanded = dropdown.IsExpanded;
        arrow.localRotation = _downRotation;
    }

    private void Update()
    {
        bool current = dropdown.IsExpanded;
        if (current != _previousExpanded)
        {
            if (current)
            {
                arrow.localRotation = _upRotation;
            }
            else
            {                
                arrow.localRotation = _downRotation;
            }

            _previousExpanded = current;
        }
    }
}