using UnityEngine;

public class TugStone : MonoBehaviour
{
    private void OnMouseDown()
    {
        SelectedStonesHandler.SetStone(gameObject);
    }
}
