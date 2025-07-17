using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabSwitcher : MonoBehaviour
{    
    [SerializeField] private List<TabEntry> tabs = new List<TabEntry>();

    [Header("Tabs visual configurations")]
    [SerializeField] private Sprite selectedImg;
    [SerializeField] private Sprite normalImg;

    [Header("Text visual configurations\"")]
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color normalColor;

    private void Awake()
    {        
        for (int i = 0; i < tabs.Count; i++)
        {
            int index = i; 
            tabs[i].TabButton.onClick.AddListener(() => OnTabClicked(index));
        }
        
        if (tabs.Count > 0)
            OnTabClicked(0);
    }

    private void OnTabClicked(int tabIndex)
    {
        for (int i = 0; i < tabs.Count; i++)
        {            
            tabs[i].TabPanel.SetActive(i == tabIndex);            
            tabs[i].TabImage.sprite = (i == tabIndex) ? selectedImg : normalImg;
            tabs[i].TabText.color = (i == tabIndex) ? selectedColor : normalColor;
        }
    }
}
