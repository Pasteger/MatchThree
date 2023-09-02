using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellsBehaviour : MonoBehaviour
{
    public GameObject cells;
    
    private readonly List<GameObject> _cells = new();

    private FieldGenerator _fieldGenerator;
    
    private void Start()
    {
        var cellsTransforms = cells.transform.GetComponentsInChildren<Transform>().ToList();
        cellsTransforms.Remove(cells.transform);
        _cells.AddRange(cellsTransforms.Select(cellTransform => cellTransform.gameObject).ToList());
        
        _fieldGenerator = gameObject.GetComponent<FieldGenerator>();
    }

    private void Update()
    {
        StoneFall();
    }

    private void StoneFall()
    {
        foreach (var cell in _cells)
        {
            var position = cell.transform.position;

            if (CheckingStoneExist(position, out _)) continue;
            
            var stone = GetStoneFromUppedCell(cell);
            stone.transform.position = new Vector3(position.x, position.y, ZPositionTypes.Stone);
            stone.GetComponent<BoxCollider>().center = Vector3.zero;
        }
    }

    private GameObject GetStoneFromUppedCell(GameObject cell)
    {
        var position = cell.transform.position;

        var uppedCell = GetUppedCell(position);
        
        if (uppedCell == null) return _fieldGenerator.SpawnStone(position.x, position.y);
        
        return CheckingStoneExist(uppedCell.transform.position, out var stone) ?
            stone : GetStoneFromUppedCell(uppedCell);
    }

    private GameObject GetUppedCell(Vector3 position)
    {
        return Physics.Raycast(new Ray(position, Vector3.up), out var cellHitInfo) ? cellHitInfo.collider.gameObject : null;
    }
    
    private bool CheckingStoneExist(Vector3 position, out GameObject stone)
    {
        if (!Physics.Raycast(new Ray(position, Vector3.back), out var stoneHitInfo))
        {
            stone = null;
            return false;
        }
        try
        {
            stone = stoneHitInfo.collider.gameObject;
            _ = stone.GetComponent<TugStone>();
            return true;
        }
        catch
        {
            stone = null;
            return false;
        }
    }
}
