using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsMenuBehaviour : MonoBehaviour
{
    public List<GameObject> buttons;
    public Sprite activeButtonSprite;
    public Sprite lockButtonSprite;
    private void Start()
    {
        for (var i = 0; i < ActiveUser.User.levelCount; i++)
        {
            buttons[i].GetComponent<Button>().interactable = true;
            
            var images = buttons[i].GetComponentsInChildren<Image>();
            foreach (var image in images)
            {
                if (image.sprite.Equals(lockButtonSprite))
                {
                    image.sprite = activeButtonSprite;
                }
            }
        }
    }

    public void MenuButtonClick()
    {
        SceneTransition.SwitchScene("MainMenu");
    }

    public void LevelButtonClick(string levelName)
    {
        SceneTransition.SwitchScene(levelName);
    }
}
