using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Vector2 _position;
    [SerializeField] private bool _isLocked;
    [SerializeField] private bool _isPainted = false;

    [SerializeField] private Material _unlockMaterial;
    [SerializeField] private Material _lockedMaterial;
    [SerializeField] private Material _paintedMaterial;

    [SerializeField] private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public Vector2 Position { get => _position; set => _position = value; }
    public bool IsLocked { get => _isLocked;}
    public bool IsPainted { get => _isPainted;}

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerContoller>())
        {
            Invoke("Paint", other.GetComponent<PlayerContoller>().AnimTime / 2);
        }
    }

    public void ChangeLock()
    {
        _isLocked = !_isLocked;

        if(_isLocked)
        {
            _meshRenderer.material = _lockedMaterial;
        }
        else
        {
            _meshRenderer.material = _unlockMaterial;
        }
    }

    public void Paint()
    {
        if(!IsLocked)
        {
            _isPainted = true;
            _meshRenderer.material = _paintedMaterial;
            GameController.inst.CheckGameResoult();
        }
    }
}
