using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoneReactionHandler : MonoBehaviour
{
    public GameObject stones;
    public readonly List<GameObject> StoneList = new();
    
    public void Update()
    {
        SetStoneList();
        
        FindMatches();
        
        DestroyStones();
        
        StoneList.Clear();
    }

    public void SetStoneList()
    {
        var stonesTransforms = stones.transform.GetComponentsInChildren<Transform>().ToList();
        stonesTransforms.Remove(stones.transform);

        StoneList.AddRange(stonesTransforms.Select(stoneTransform => stoneTransform.gameObject).ToList());

    }
    
    public void FindMatches()
    {
        foreach (var stone in StoneList)
        {
            var stoneTexture = stone.GetComponent<MeshRenderer>().material.mainTexture;
            
            if (Physics.Raycast(new Ray(stone.transform.position, Vector3.up), out var upHitInfo, 1f) && 
                Physics.Raycast(new Ray(stone.transform.position, Vector3.down), out var downHirInfo, 1f) 
                && upHitInfo.collider.gameObject.GetComponent<MeshRenderer>().material.mainTexture.Equals(stoneTexture) &&
                downHirInfo.collider.gameObject.GetComponent<MeshRenderer>().material.mainTexture.Equals(stoneTexture)
                && !upHitInfo.collider.gameObject.Equals(stone) && !downHirInfo.collider.gameObject.Equals(stone))
            {
                stone.tag = "active";
                upHitInfo.collider.gameObject.tag = "active";
                downHirInfo.collider.gameObject.tag = "active";
            }
            
            if (Physics.Raycast(new Ray(stone.transform.position, Vector3.left), out var leftHitInfo, 1f) && 
                Physics.Raycast(new Ray(stone.transform.position, Vector3.right), out var rightHirInfo, 1f) 
                && leftHitInfo.collider.gameObject.GetComponent<MeshRenderer>().material.mainTexture.Equals(stoneTexture) &&
                rightHirInfo.collider.gameObject.GetComponent<MeshRenderer>().material.mainTexture.Equals(stoneTexture)
                && !leftHitInfo.collider.gameObject.Equals(stone) && !rightHirInfo.collider.gameObject.Equals(stone))
            {
                stone.tag = "active";
                leftHitInfo.collider.gameObject.tag = "active";
                rightHirInfo.collider.gameObject.tag = "active";
            }
        }
    }
    
    private void DestroyStones()
    {
        foreach (var stone in StoneList.Where(stone => stone.tag.Equals("active")))
        {
            Destroy(stone);
        }
    }
}
