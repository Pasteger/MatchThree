using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBehaviour : MonoBehaviour
{
    public void NewGameButtonClick()
    {
        SceneTransition.SwitchScene("Level1");
    }
    
    public void ExitButtonClick()
    {
        Application.Quit();
    }
}
