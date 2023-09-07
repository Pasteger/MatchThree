using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchBehaviour : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "MainMenu")
		{
			SceneTransition.SwitchScene("MainMenu");
		}
    }
}
