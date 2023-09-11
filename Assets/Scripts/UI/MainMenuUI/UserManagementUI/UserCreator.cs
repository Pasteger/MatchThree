using UnityEngine;
using UnityEngine.UI;

public class UserCreator : MonoBehaviour
{
    private UserDataManager _userDataManager;
    
    public InputField usernameInputField;

    public SelectAvatarListBehaviour selectAvatarListBehaviour;

    public UserListBehaviour userListBehaviour;
    
    private void Awake()
    {
        _userDataManager = UserDataManager.GetInstance();
    }
    
    public void CreateUser()
    {
        var username = usernameInputField.text;
        usernameInputField.text = "";
        
        if (username.Length < 2) return;
        
        var avatar = selectAvatarListBehaviour.GetAvatarName() ?? Random.Range(0, 8).ToString();

        var user = new UserData
        {
            username = username,
            levelCount = 0,
            avatarName = avatar
        };
        
        ActiveUser.User = _userDataManager.AddUser(user);
        
        userListBehaviour.ReloadButtons();
        
        HideUserCreator();
    }
    
    public void HideUserCreator()
    {
        gameObject.SetActive(false);
    }
}
