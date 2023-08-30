using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class FieldGenerator : MonoBehaviour
{
    public List<Texture> textures;
    public GameObject stonePrefab;
    public GameObject stones;
    public GameObject cells;

    private Material _material;

    private void Start()
    {
        _material = stonePrefab.GetComponent<MeshRenderer>().sharedMaterial;
        
        var cellsTransforms = cells.transform.GetComponentsInChildren<Transform>().ToList();
        cellsTransforms.Remove(cells.transform);

        foreach (var position in cellsTransforms.Select(cellTransform => cellTransform.position))
        {
            GenerateStone(position.x, position.y);
        }
    }

    public void SpawnStone(float x, float y)
    {
        var stone = Instantiate(stonePrefab, new Vector3(x, y, ZPositionTypes.Stone), Quaternion.identity);
        stone.GetComponent<MeshRenderer>().sharedMaterial = new Material(_material)
        {
            mainTexture = textures[Random.Range(0, textures.Count)]
        };
        stone.transform.SetParent(stones.transform);
    }

    private void GenerateStone(float x, float y)
    {
        Texture texture;
        var position = new Vector3(x, y, ZPositionTypes.Stone);
        
        var stone = Instantiate(stonePrefab, position, Quaternion.identity);
        
        bool neighboringTexture;
        var iteration = 0;
        do
        {
            texture = textures[Random.Range(0, textures.Count)];

            neighboringTexture = Physics.Raycast(new Ray(position, Vector3.up), out var hitInfo, 1f)
                      && hitInfo.collider.gameObject.GetComponent<MeshRenderer>().sharedMaterial.mainTexture
                          .Equals(texture)
                      || Physics.Raycast(new Ray(position, Vector3.down), out hitInfo, 1f)
                      && hitInfo.collider.gameObject.GetComponent<MeshRenderer>().sharedMaterial.mainTexture
                          .Equals(texture)
                      || Physics.Raycast(new Ray(position, Vector3.right), out hitInfo, 1f)
                      && hitInfo.collider.gameObject.GetComponent<MeshRenderer>().sharedMaterial.mainTexture
                          .Equals(texture)
                      || Physics.Raycast(new Ray(position, Vector3.left), out hitInfo, 1f)
                      && hitInfo.collider.gameObject.GetComponent<MeshRenderer>().sharedMaterial.mainTexture
                          .Equals(texture);
            
            iteration++;
        }
        while (neighboringTexture && iteration < 20);
        
        
        stone.GetComponent<MeshRenderer>().sharedMaterial = new Material(_material)
        {
            mainTexture = texture
        };
        
        stone.transform.SetParent(stones.transform);
    }
}
