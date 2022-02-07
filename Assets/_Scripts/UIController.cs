using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;

    private void Start()
    {
        GameController.inst.OnGameWin += ShowGamePanel;
    }

    private void ShowGamePanel()
    {
        _winPanel.SetActive(true);
    }
}
