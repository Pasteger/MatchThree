using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFild : MonoBehaviour
{
    public int gridWidth = 14;
    public int gridHeight = 14;
    public GameObject[] examplesStones = new GameObject[8];
    public List<GameObject> examples = new List<GameObject>();
    public List<GameObject> stones = new List<GameObject>();
    public float stoneRadius = 0.642f;

    void Awake()
    {
        examples.AddRange(examplesStones);
        for (int j = 0; j < gridHeight; j++)
        {
            for (int i = 0; i < gridWidth; i++)
            {
                GameObject stone = null;
                int a = 0;
                while (a < 5)
                {
                    a++;
                    stone = examplesStones[UnityEngine.Random.Range(0, examplesStones.Length)];
                    int indexThisStone = j * gridWidth + i;
                    string thisStoneSpriteName = stone.GetComponent<SpriteRenderer>().sprite.name;
                    try
                    {
                        if (stones[indexThisStone - 1].GetComponent<SpriteRenderer>().sprite.name.Equals(thisStoneSpriteName))
                        {
                            continue;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                    try
                    {
                        if (stones[indexThisStone - gridWidth].GetComponent<SpriteRenderer>().sprite.name.Equals(thisStoneSpriteName))
                        {
                            continue;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                    break;
                }

                stones.Add(Instantiate(stone,
                new Vector3(-4.17f + (stoneRadius * i), -4.17f + (stoneRadius * j), -1),
                Quaternion.Euler(0, 0, 0)
            ));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GenerateNewStone(Vector3 position)
    {
        GameObject stone = examplesStones[UnityEngine.Random.Range(0, examplesStones.Length)];
        return Instantiate(stone, position, Quaternion.Euler(0, 0, 0));
    }
}
