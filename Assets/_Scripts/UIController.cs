using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _buttonControl;

    private void Start()
    {
        GameController.inst.OnGameWin += ShowGamePanel;
    }

    private void ShowGamePanel()
    {
        _winPanel.SetActive(true);
        _buttonControl.SetActive(false);
    }
}
