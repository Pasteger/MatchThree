using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoneReactionHandler : MonoBehaviour
{
    public GameObject stones;
    
    private readonly List<GameObject> _stoneList = new();
    
    private ScoreCalculator _scoreCalculator;
    
    public bool isAnimation;
    private bool _isUndo;
    private int _animationCounter;

    private GameObject _firstSwappedStone;
    private GameObject _secondSwappedStone;
    
    private Vector3 _startFirstStonePosition;
    private Vector3 _startSecondStonePosition;
    
    private void Awake()
    {
        _scoreCalculator = GetComponent<ScoreCalculator>();
    }
    
    public void FixedUpdate()
    {
        SetStoneList();
        
        PrepareSwapStones();
        
        if (isAnimation)
        {
            SwapStones();
        }
        else
        {
            FindMatches();

            DestroyStones();

            UndoSwapStones();
        }

        _stoneList.Clear();
    }

    private void PrepareSwapStones()
    {
        if (_firstSwappedStone != null || _secondSwappedStone != null) return;
        
        var swappedStones = _stoneList.Where(stone => 
            stone.CompareTag("Swapped")).ToList();
        if (swappedStones.Count < 2) return;
        
        _firstSwappedStone = swappedStones[0];
        _secondSwappedStone = swappedStones[1];

        _startFirstStonePosition = _firstSwappedStone.transform.position;
        _startSecondStonePosition = _secondSwappedStone.transform.position;

        _animationCounter = 0;
        isAnimation = true;
    }
    
    private void SwapStones()
    {
        if (_animationCounter > 100)
        {
            isAnimation = false;
            SelectedStonesHandler.pause = false;
            if (_isUndo)
            {
                _isUndo = false;
                _firstSwappedStone.tag = "Untagged";
                _secondSwappedStone.tag = "Untagged";
                _firstSwappedStone = null;
                _secondSwappedStone = null;
                _animationCounter = 0;
            }
            return;
        }

        _firstSwappedStone.transform.position = Vector3.Lerp(_startFirstStonePosition, _startSecondStonePosition, (float) _animationCounter / 100);
        _secondSwappedStone.transform.position = Vector3.Lerp(_startSecondStonePosition, _startFirstStonePosition, (float) _animationCounter / 100);

        _firstSwappedStone.GetComponent<BoxCollider>().center = Vector3.zero;
        _secondSwappedStone.GetComponent<BoxCollider>().center = Vector3.zero;
        
        _animationCounter += 5;
    }
    
    private void UndoSwapStones()
    {
        _animationCounter = 0;

        if (_firstSwappedStone == null && _secondSwappedStone == null) return;
        
        if (_secondSwappedStone == null && _firstSwappedStone != null)
        {
            _firstSwappedStone.tag = "Untagged";
            _firstSwappedStone = null;
            return;
        }
        if (_firstSwappedStone == null && _secondSwappedStone != null)
        {
            _secondSwappedStone.tag = "Untagged";
            _secondSwappedStone = null;
            return;
        }
        
        (_firstSwappedStone, _secondSwappedStone) = (_secondSwappedStone, _firstSwappedStone);
        isAnimation = true;
        _isUndo = true;
        SelectedStonesHandler.pause = true;
    }


    private void SetStoneList()
    {
        var stonesTransforms = stones.transform.GetComponentsInChildren<Transform>().ToList();
        stonesTransforms.Remove(stones.transform);

        _stoneList.AddRange(stonesTransforms.Select(stoneTransform => stoneTransform.gameObject).ToList());
    }

    private void FindMatches()
    {
        foreach (var stone in _stoneList)
        {
            var stoneTexture = stone.GetComponent<MeshRenderer>().material.mainTexture;

            if (Physics.Raycast(new Ray(stone.transform.position, Vector3.up), out var upHitInfo, 1f) &&
                Physics.Raycast(new Ray(stone.transform.position, Vector3.down), out var downHirInfo, 1f)
                && upHitInfo.collider.gameObject.GetComponent<MeshRenderer>().material.mainTexture
                    .Equals(stoneTexture) &&
                downHirInfo.collider.gameObject.GetComponent<MeshRenderer>().material.mainTexture.Equals(stoneTexture)
                && !upHitInfo.collider.gameObject.Equals(stone) && !downHirInfo.collider.gameObject.Equals(stone))
            {
                stone.tag = "Active";
                upHitInfo.collider.gameObject.tag = "Active";
                downHirInfo.collider.gameObject.tag = "Active";
            }

            if (Physics.Raycast(new Ray(stone.transform.position, Vector3.left), out var leftHitInfo, 1f) &&
                Physics.Raycast(new Ray(stone.transform.position, Vector3.right), out var rightHirInfo, 1f)
                && leftHitInfo.collider.gameObject.GetComponent<MeshRenderer>().material.mainTexture
                    .Equals(stoneTexture) &&
                rightHirInfo.collider.gameObject.GetComponent<MeshRenderer>().material.mainTexture.Equals(stoneTexture)
                && !leftHitInfo.collider.gameObject.Equals(stone) && !rightHirInfo.collider.gameObject.Equals(stone))
            {
                stone.tag = "Active";
                leftHitInfo.collider.gameObject.tag = "Active";
                rightHirInfo.collider.gameObject.tag = "Active";
            }
        }
    }

    private void DestroyStones()
    {
        var stonesForDestroy = _stoneList.Where(stone =>
            stone.CompareTag("Active")).ToList();
        
        if (stonesForDestroy.Count == 0) return;
        
        
        foreach (var stone in _stoneList)
        {
            stone.tag = "Untagged";
        }

        _firstSwappedStone = null;
        _firstSwappedStone = null;
        
        _scoreCalculator.AddScore(stonesForDestroy.Count);
        
        stonesForDestroy.ForEach(Destroy);
    }
}
