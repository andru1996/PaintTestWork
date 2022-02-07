using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController inst;

    [SerializeField] private PoolCells _poolCells;

    [HideInInspector]
    [SerializeField] private PlayerContoller _player;

    public System.Action OnGameWin;

    public PlayerContoller Player { get => _player; set => _player = value; }

    private void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        _player.OnPlayerMove += CheckGameResoult;
    }

    public void CheckGameResoult()
    {
        bool isWin = true;
        foreach (Cell cell in _poolCells.CellList)
        {
            if (!cell.IsLocked && !cell.IsPainted)
            {
                isWin = false;
            } 
        }

        if(isWin)
        {
            GameWin();
        }
    }

    private void GameWin()
    {
        OnGameWin?.Invoke();
        GetComponent<SwipeDetection>().enabled = false;
    }

}
