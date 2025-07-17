using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        closeButton.onClick.AddListener(Close);
    }

    public void Close()
    {
        //gameObject.SetActive(false);
        Debug.Log("Close button is pressed");
    }
}
