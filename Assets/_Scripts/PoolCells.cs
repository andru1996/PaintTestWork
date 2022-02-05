using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolCells : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private List<Cell> _cellList;
    [SerializeField] private Vector2 _size;
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Vector2 _playerStartPosition;

    private GameObject _player;
    public Vector2 Size { get => _size; }
    public List<Cell> CellList { get => _cellList; set => _cellList = value; }

    public void Generate()
    {
        _cellList = new List<Cell>();
        for(int x = 0; x <= _size.x-1; x++)
        {
            for(int y = 0; y<= _size.y-1; y++)
            {
                Cell cell = Instantiate(_cellPrefab, new Vector3(x, y, 1f), _cellPrefab.transform.rotation, transform).GetComponent<Cell>();
                cell.name = "(" + x + "," + y + ")";
                cell.Position = new Vector2(x, y);

                _cellList.Add(cell);
            }
        }
        if(_playerStartPosition.x >= _size.x || _playerStartPosition.y >= _size.y)
        {
            DestroyImmediate(_player);
        }
    }

    public void DeleteAllCells()
    {
        Cell[] cells = GetComponentsInChildren<Cell>();
        foreach(Cell cell in cells)
        {
            DestroyImmediate(cell.gameObject);
        }
    }

    public void SetPlayer()
    {
        if(_playerStartPosition.x < 0 || _playerStartPosition.y < 0 || _playerStartPosition.x>=_size.x || _playerStartPosition.y >= _size.y)
        {
            Debug.LogError("неверная позиция игрока");
            return;
        }

        if(_player == null)
        {
            _player = Instantiate(_playerPrefab, new Vector3(_playerStartPosition.x, _playerStartPosition.y, 0.1f), _playerPrefab.transform.rotation);
            _player.GetComponent<PlayerContoller>().PoolCells = this;
            gameController.Player = _player.GetComponent<PlayerContoller>();
        }
        else
        {
            _player.transform.position = new Vector3(_playerStartPosition.x, _playerStartPosition.y, 0.1f);
        }

        _player.GetComponent<PlayerContoller>().Position = _playerStartPosition;
    }
}
