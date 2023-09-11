using System;
using UnityEngine;

public static class SelectedStonesHandler 
{
    private static GameObject _selectedStone;
    private static GameObject _targetStone;
    
    private static void SwapStones()
    {
        Swap();
        
        UndoSetStone();
    }

    private static void Swap()
    {
        var selectedStonePosition = _selectedStone.transform.position;
        var targetStonePosition = _targetStone.transform.position;

        if ((!Math.Abs(selectedStonePosition.x - targetStonePosition.x).Equals(1f) ||
             !(selectedStonePosition.y - targetStonePosition.y).Equals(0)) &&
            (!Math.Abs(selectedStonePosition.y - targetStonePosition.y).Equals(1f) ||
             !(selectedStonePosition.x - targetStonePosition.x).Equals(0)))
        {
            return;
        }
        
        _selectedStone.tag = "Swaped";
        _targetStone.tag = "Swaped";
        

        _selectedStone.transform.position = targetStonePosition;
        _targetStone.transform.position = selectedStonePosition;
        
        _selectedStone.GetComponent<BoxCollider>().center = Vector3.zero;
        _targetStone.GetComponent<BoxCollider>().center = Vector3.zero;
    }

    public static void SetStone(GameObject stone)
    {
        if (_selectedStone == null)
        {
            _selectedStone = stone;
            var localScale = _selectedStone.transform.localScale;
            localScale = new Vector3(
                localScale.x + 0.1f,
                localScale.x + 0.1f, 1);
            _selectedStone.transform.localScale = localScale;
        }
        else if (_selectedStone.Equals(stone))
        {
            UndoSetStone();
        }
        else 
        {
            _targetStone = stone;
            SwapStones();
        }
    }

    private static void UndoSetStone()
    {
        var localScale = _selectedStone.transform.localScale;
        localScale = new Vector3(
            localScale.x - 0.1f,
            localScale.x - 0.1f, 1);
        _selectedStone.transform.localScale = localScale;
        _selectedStone = null;
        _targetStone = null;
    }
}
