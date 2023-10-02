using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoneReactionHandler : MonoBehaviour
{
    public GameObject stones;
    
    private readonly List<GameObject> _stoneList = new();
    
    private ScoreCalculator _scoreCalculator;

    private void Awake()
    {
        _scoreCalculator = GetComponent<ScoreCalculator>();
    }
    
    public void Update()
    {
        SetStoneList();

        FindMatches();

        DestroyStones();

        UndoSwapStones();

        _stoneList.Clear();
    }

    private void UndoSwapStones()
    {
        foreach (var stone in _stoneList.Where(stone => stone.CompareTag("Swaped")))
        {
            SelectedStonesHandler.SetStone(stone);
        }
        foreach (var stone in _stoneList.Where(stone => stone.CompareTag("Swaped")))
        {
            stone.tag = "Untagged";
        }
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
        
        _scoreCalculator.AddScore(stonesForDestroy.Count);
        
        stonesForDestroy.ForEach(Destroy);
    }
}
