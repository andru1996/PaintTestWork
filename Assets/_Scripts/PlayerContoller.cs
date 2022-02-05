using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerContoller : MonoBehaviour
{
    [SerializeField] private PoolCells _poolCells;
    [SerializeField] private Cell[,] _cells;
    [SerializeField] private Vector2 _position;
    [SerializeField] private float animTime;

    private bool isMove = false;

    public System.Action OnPlayerMove;

    public Vector2 Position { get => _position; set => _position = value; }
    public Cell[,] Cells { get => _cells; set => _cells = value; }
    public PoolCells PoolCells { get => _poolCells; set => _poolCells = value; }

    private void Start()
    {
        InitCells();
    }

    private void InitCells()
    {
        Vector2 size = _poolCells.Size;
        _cells = new Cell[(int)size.x, (int)size.y];
        int m = 0;
        for (int x = 0; x <= size.x - 1; x++)
        {
            for (int y = 0; y <= size.y - 1; y++)
            {
                _cells[x, y] = _poolCells.CellList[m];
                m++;
            }
        }
    }

    public void Move(string diraction)
    {
        if (isMove) return;

        List<Cell> targetCells = new List<Cell>();
        Cell pastCell = _cells[(int)_position.x, (int)_position.y];
        targetCells.Add(pastCell);

        switch (diraction)
        {
            case "Up":
                for(int i = (int)_position.y + 1; i <= _poolCells.Size.y-1; i++)
                {
                    Cell cell = _cells[(int)_position.x, i];
                    if (!cell.IsLocked)
                    {
                        targetCells.Add(cell);
                        pastCell = cell;
                    }
                    else
                    {
                        break;
                    }
                }
                break;
            case "Down":
                for (int i = (int)_position.y - 1; i >= 0; i--)
                {
                    Cell cell = _cells[(int)_position.x, i];
                    if (!cell.IsLocked)
                    {
                        targetCells.Add(cell);
                        pastCell = cell;
                    }
                    else
                    {
                        break;
                    }
                }
                break;
            case "Left":
                for (int i = (int)_position.x - 1; i >= 0; i--)
                {
                    Cell cell = _cells[i, (int)_position.y];
                    if (!cell.IsLocked)
                    {
                        targetCells.Add(cell);
                        pastCell = cell;
                    }
                    else
                    {
                        break;
                    }
                }
                break;
            case "Right":
                for (int i = (int)_position.x + 1; i <= _poolCells.Size.x - 1; i++)
                {
                    Cell cell = _cells[i, (int)_position.y];
                    if (!cell.IsLocked)
                    {
                        targetCells.Add(cell);
                        pastCell = cell;
                    }
                    else
                    {
                        break;
                    }
                }
                break;
            default:
                break;
        }

        if(targetCells.Count > 1)
        {
            isMove = true;
            Vector3 targetPos = new Vector3(pastCell.gameObject.transform.position.x, pastCell.gameObject.transform.position.y, 0.1f);

            transform.DOMove(targetPos, animTime * targetCells.Count, false)
            .OnComplete(() =>
            {
                isMove = false;
            });
            _position = pastCell.Position;
        }
    }
}
