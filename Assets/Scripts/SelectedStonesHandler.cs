using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectedStonesHandler : MonoBehaviour
{
    private static GameObject selectedStone;
    private static GameObject targetStone;

    // Update is called once per frame
    void Update()
    {
        if (targetStone != null)
        {
            SwapStones();
        }
    }

    private void SwapStones() 
    {
        List<GameObject> stones = gameObject.GetComponent<GenerateFild>().stones;
        int width = gameObject.GetComponent<GenerateFild>().gridWidth;

        int selectedStoneIndex = stones.IndexOf(selectedStone);
        int targetStoneIndex = stones.IndexOf(targetStone);

        int distanse = selectedStoneIndex - targetStoneIndex;
        if (distanse == 1 || distanse == -1 || distanse == width || distanse == -width) 
        {
            Vector3 selectedStonePosition = selectedStone.transform.position;
            Vector3 targetStonePosition = targetStone.transform.position;

            selectedStone.transform.position = targetStonePosition;
            targetStone.transform.position = selectedStonePosition;

            stones[selectedStoneIndex] = targetStone;
            stones[targetStoneIndex] = selectedStone;
        }

        UndoSetStone();
    }

    public void SetStone(GameObject stone)
    {
        if (selectedStone == null)
        {
            selectedStone = stone;
            selectedStone.transform.localScale = new Vector3(
                selectedStone.transform.localScale.x + 0.1f,
                selectedStone.transform.localScale.x + 0.1f, 0);
        }
        else if (selectedStone.Equals(stone))
        {
            UndoSetStone();
        }
        else 
        {
            targetStone = stone;
        }
    }

    public void UndoSetStone()
    {
        selectedStone.transform.localScale = new Vector3(
            selectedStone.transform.localScale.x - 0.1f,
            selectedStone.transform.localScale.x - 0.1f, 0);
        selectedStone = null;
        targetStone = null;
    }
}
