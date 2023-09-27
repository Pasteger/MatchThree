using UnityEngine;

public class SceneSwitchBehaviour : MonoBehaviour
{
	public LevelMenuBehaviour levelMenuBehaviour;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !levelMenuBehaviour.IsLocked())
		{
			levelMenuBehaviour.gameObject.SetActive(!levelMenuBehaviour.gameObject.activeSelf);
		}
    }
}
