using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIBehaviour : MonoBehaviour
{
    public GameObject loadGameButton;
    public GameObject levelsMenuButton;
    
    public GameObject userController;
    public Text usernameText;
    public SpriteRenderer userAvatar;

    public SelectAvatarListBehaviour selectAvatarListBehaviour;
    
    private List<Sprite> _avatars;

    private Button _loadGameComponentButton;
    private Button _levelsMenuComponentButton;
    private void Start()
    {
        _avatars = selectAvatarListBehaviour.avatars;
        _loadGameComponentButton = loadGameButton.GetComponent<Button>();
        _levelsMenuComponentButton = levelsMenuButton.GetComponent<Button>();

    }

    public void LoadGameButtonClick()
    {
        SceneTransition.SwitchScene("Level" + ActiveUser.User.levelCount);
    }
    
    public void LevelsMenuButtonClick()
    {
        SceneTransition.SwitchScene("LevelsMenu");
    }

    public void NewGameButtonClick()
    {
        if (ActiveUser.User.username == null)
        {
            userController.SetActive(true);
            userController.transform.GetComponentInChildren<UserListBehaviour>().ReloadButtons();        }
        else
        {
            SceneTransition.SwitchScene("Level1");
        }
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }

    public void SwitchUserButtonClick()
    {
        userController.SetActive(true);
        userController.transform.GetComponentInChildren<UserListBehaviour>().ReloadButtons();
    }

    private void Update()
    {
        _loadGameComponentButton.interactable  = ActiveUser.User.levelCount > 0;
        _levelsMenuComponentButton.interactable  = ActiveUser.User.levelCount > 0;
        
        usernameText.text = ActiveUser.User.username;
        
        var avatar = GetUserAvatar(ActiveUser.User.avatarName);
        if (avatar != null)
        {
            userAvatar.gameObject.SetActive(true);

            userAvatar.sprite = avatar;
        }
        else
        {
            userAvatar.gameObject.SetActive(false);
        }
    }

    private Sprite GetUserAvatar(string avatarName)
    {
        Sprite sprite = null;
        foreach (var avatar in _avatars.Where(avatar => avatar.name == avatarName))
        {
            sprite = avatar;
        }

        return sprite;
    }
}
