using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TugStone : MonoBehaviour
{
    [SerializeField] public GameObject grid;
    SelectedStonesHandler selectedStonesHandler;

    private void Start()
    {
        selectedStonesHandler = grid.GetComponent<SelectedStonesHandler>();
    }

    void OnMouseDown()
    {
        selectedStonesHandler.SetStone(gameObject);
    }
}
