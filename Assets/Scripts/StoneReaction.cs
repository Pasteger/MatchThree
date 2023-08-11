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
    //<Id first stone in stones, number of stones in column>
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

    }

    void Update()
    {
        //HorizontalCheking();
        //VerticalCheking();
        Checking();

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

    void Checking()
    {
        for(int index = 0; index < stones.Count; index++)
        {
            float row = index / gridHeight;
            float column = index % gridWidth;

            if ((row == 0 || row == gridWidth - 1) && (column == 0 || column == gridHeight - 1))
            {
                continue;
            }

            int position = 0;
            if (row == 0 || row == gridWidth - 1)
            {
                position = 1;
            }
            else if (column == 0 || column == gridHeight - 1)
            {
                position = -1;
            }

            Sprite stoneSprite = stones[index].GetComponent<SpriteRenderer>().sprite;

            
            if (position > -1 && 
                stones[index - 1].GetComponent<SpriteRenderer>().sprite.Equals(stoneSprite) &&
                stones[index + 1].GetComponent<SpriteRenderer>().sprite.Equals(stoneSprite))
            {
                stones[index].tag = "activeHorisontal";
               
            }
            if (position < 1 && 
                stones[index - gridWidth].GetComponent<SpriteRenderer>().sprite.Equals(stoneSprite) &&
                stones[index + gridWidth].GetComponent<SpriteRenderer>().sprite.Equals(stoneSprite))
            {
                stones[index].tag = "activeVertical";
            }
        }

        for (int index = 0; index < stones.Count; index++)
        {
            Sprite stoneSprite = stones[index].GetComponent<SpriteRenderer>().sprite;
            try
            {
                if (stones[index - 1].tag.Contains("active") && 
                    stones[index - 1].GetComponent<SpriteRenderer>().sprite.Equals(stoneSprite))
                {
                    stones[index].tag = "activeHorisontal";
                    continue;
                }
            }
            catch { }
            try
            {
                if (stones[index + 1].tag.Contains("active") &&
                    stones[index + 1].GetComponent<SpriteRenderer>().sprite.Equals(stoneSprite))
                {
                    stones[index].tag = "activeHorisontal";
                    continue;
                }
            }
            catch { }
            try
            {
                if (stones[index - gridWidth].tag.Contains("active") &&
                    stones[index - gridWidth].GetComponent<SpriteRenderer>().sprite.Equals(stoneSprite))
                {
                    stones[index].tag = "activeVertical";
                    continue;
                }
            }
            catch { }
            try
            {
                if (stones[index + gridWidth].tag.Contains("active") &&
                    stones[index + gridWidth].GetComponent<SpriteRenderer>().sprite.Equals(stoneSprite))
                {
                    stones[index].tag = "activeVertical";
                    continue;
                }
            }
            catch { }
        }

        for (int index = 0; index < stones.Count; index++)
        {
            if (stones[index].tag.Equals("activeHorisontal"))
            {
                try
                {
                    if (stones[index - 1].tag.Equals("activeHorisontal"))
                    {
                        continue;
                    }
                }
                catch { }

                int count = 1;
                int nextIndex = index;
                while (true)
                {
                    nextIndex++;
                    try
                    {
                        if (stones[nextIndex].tag.Equals("activeHorisontal"))
                        {
                            count++;
                        }
                        else
                        {
                            horisontalStones.Add(index, count);
                            break;
                        }
                    }
                    catch
                    {
                        try
                        {
                            horisontalStones.Add(index, count);
                        }
                        catch { }
                        break;
                    }
                }
            }
            if (stones[index].tag.Equals("activeVertical"))
            {
                try
                {
                    if (stones[index - gridWidth].tag.Equals("activeVertical"))
                    {
                        continue;
                    }
                }
                catch { }

                int count = 1;
                int nextIndex = index;
                while (true)
                {
                    nextIndex += gridWidth;
                    try
                    {
                        if (stones[nextIndex].tag.Equals("activeVertical"))
                        {
                            count++;
                        }
                        else
                        {
                            verticalStones.Add(index, count);
                            break;
                        }
                    }
                    catch
                    {
                        try
                        {
                            verticalStones.Add(index, count);
                        }
                        catch { }
                        break;
                    }
                }
            }
        }
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


}
