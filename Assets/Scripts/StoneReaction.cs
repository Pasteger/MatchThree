using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoneReaction : MonoBehaviour
{
    private List<GameObject> stones;
    public GameObject scoreText;
    private int score = 0;
    //<Id first stone in stones, number of stones in row>
    Dictionary<int, int> horisontalStones = new Dictionary<int, int>();
    Dictionary<int, int> verticalStones = new Dictionary<int, int>();
    int gridWidth;
    int gridHeight;
    float stoneRadius;
    void Start()
    {
        stones = gameObject.GetComponent<GenerateFild>().stones;
        gridWidth = gameObject.GetComponent<GenerateFild>().gridWidth;
        gridHeight = gameObject.GetComponent<GenerateFild>().gridHeight;
        stoneRadius = gameObject.GetComponent<GenerateFild>().stoneRadius;


        //StartCoroutine(Test());
    }

    void Update()
    {
        HorizontalCheking();
        VerticalCheking();

        for (int i = 0; i < stones.Count; i++)
        {
            try
            {
                int countOfStones = horisontalStones[i];

                HorizontalReconfigurationStones(i, countOfStones);
            }
            catch (Exception)
            {
                continue;
            }
        }

        for (int i = 0; i < stones.Count; i++)
        {
            try
            {
                int countOfStones = verticalStones[i];

                VerticalReconfigurationStones(i, countOfStones);
            }
            catch (Exception)
            {
                continue;
            }
        }

        scoreText.GetComponent<Text>().text = "Score: " + score;

        horisontalStones.Clear();
        verticalStones.Clear();
    }

    void HorizontalCheking()
    {
        string previousStoneSpriteName = null;
        int counter = 0;
        foreach (GameObject stone in stones)
        {
            if (stones.IndexOf(stone) == 0)
            {
                counter++;
            }
            else if (stone.GetComponent<SpriteRenderer>().sprite.name.Equals(previousStoneSpriteName))
            {
                counter++;
                if (stones.IndexOf(stone) % gridWidth == 0)
                {
                    counter = 1;
                }
            }
            else
            {
                if (counter > 2)
                {
                    horisontalStones.Add(stones.IndexOf(stone) - counter, counter);
                }
                counter = 1;
            }
            previousStoneSpriteName = stone.GetComponent<SpriteRenderer>().sprite.name;
        }
    }

    void VerticalCheking()
    {
        string previousStoneSpriteName = null;
        int counter = 0;
        for (int i = 0; i < gridHeight; i++)
        {
            for (int j = 0; j < gridHeight * gridWidth; j += gridWidth)
            {
                if (j + i == 0)
                {
                    counter++;
                }
                else if (stones[j + i].GetComponent<SpriteRenderer>().sprite.name.Equals(previousStoneSpriteName))
                {
                    counter++;
                }
                else
                {
                    if (counter > 2)
                    {
                        verticalStones.Add(j + i - counter * gridWidth, counter);
                        Debug.Log(j + " + " + i + " - " + counter + " * " + gridWidth);
                        Debug.Log(j + i - counter * gridWidth + "   " + counter);
                    }
                    counter = 1;
                }
                previousStoneSpriteName = stones[j + i].GetComponent<SpriteRenderer>().sprite.name;
            }
        }
    }

    void HorizontalReconfigurationStones(int index, int count)
    {
        Vector3[] positions = new Vector3[count];
        for (int i = 0; i < count; i++)
        {
            positions[i] = stones[index + i].GetComponent<Transform>().position;
            Destroy(stones[index + i]);
            score++;
        }
        int id = index;
        while (id < stones.Count)
        {
            for (int i = 0; i < count; i++)
            {
                int hightIndex = id + gridHeight + i;
                try
                {
                    Vector3 position = stones[hightIndex].GetComponent<Transform>().position;
                    stones[hightIndex].GetComponent<Transform>().position = positions[i];
                    positions[i] = position;

                    stones[id + i] = stones[hightIndex];
                }
                catch
                {
                    stones[id + i] = gameObject.GetComponent<GenerateFild>()
                        .GenerateNewStone(positions[i]);
                }
            }
            id += gridHeight;
        }
    }

    void VerticalReconfigurationStones(int index, int count)
    {
        Vector3 rootPosition = stones[index].GetComponent<Transform>().position; ;
        for (int i = 0; i < count; i++)
        {
            Destroy(stones[index + (i * gridWidth)]);
            score++;
        }
        int id = index;
        while (id < stones.Count)
        {
            try
            {
                int hightIndex = id + gridWidth * count;

                Vector3 position = stones[hightIndex].GetComponent<Transform>().position;
                stones[hightIndex].GetComponent<Transform>().position = rootPosition;

                stones[id] = stones[hightIndex];
            }
            catch
            {
                stones[id] = gameObject.GetComponent<GenerateFild>()
                    .GenerateNewStone(rootPosition);
            }
            rootPosition.y += stoneRadius;
            id += gridWidth;
        }
    }

    int i = 0;
    private IEnumerator Test()
    {
        while (true)
        {
            try
            {
                GameObject stone = stones[i];
                Debug.Log(i);
                stone.transform.localScale = new Vector3(
                        stone.transform.localScale.x + 0.1f,
                        stone.transform.localScale.x + 0.1f, 0);
                i++;
            }
            catch
            {
                i = 0;
                Debug.Log("i = 0");
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
