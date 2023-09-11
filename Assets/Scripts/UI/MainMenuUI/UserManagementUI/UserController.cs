using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UserController : MonoBehaviour
{
    public GameObject userCreator;

    public Text usernameText;
    public GameObject deleteUserButton;
    public Image userAvatar;

    public SelectAvatarListBehaviour selectAvatarListBehaviour;

    private UserDataManager _userDataManager;

    private List<Sprite> _avatars;

    private void Awake()
    {
        _userDataManager = UserDataManager.GetInstance();
    }

    private void Start()
    {
        _avatars = selectAvatarListBehaviour.avatars;
    }
    
    public void HideUserController()
    {
        gameObject.SetActive(false);
    }

    public void ShowUserCreator()
    {
        userCreator.SetActive(true);
    }

    public void DeleteUser()
    {
        if (!_userDataManager.DeleteUser(ActiveUser.User)) return;
        transform.GetComponentInChildren<UserListBehaviour>().ReloadButtons();
        ActiveUser.User = new UserData();
    }

    private void Update()
    {
        usernameText.text = ActiveUser.User.username;
        deleteUserButton.SetActive(ActiveUser.User.username != null);

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