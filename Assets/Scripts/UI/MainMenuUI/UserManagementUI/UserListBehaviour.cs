using UnityEngine;
using UnityEngine.UI;

public class UserListBehaviour : MonoBehaviour
{
    public GameObject userButtonPrefab;

    private UserDataManager _userDataManager;

    private void Awake()
    {
        _userDataManager = UserDataManager.GetInstance();
    }

    public void ReloadButtons()
    {
        RemoveButtons();
        var users = _userDataManager.GetUsers();
        foreach (var user in users)
        {
            CreateButton(user);
        }
        
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            if (transform.GetChild(i).GetComponentInChildren<Text>().text !=
                ActiveUser.User.username) continue;
            transform.GetChild(i).GetComponent<Button>().interactable = false;
            return;
        }
    }

    private void CreateButton(UserData user)
    {
        var userButton = Instantiate(userButtonPrefab, transform);
        userButton.GetComponentInChildren<Text>().text = user.username;
        
        userButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            ActiveUser.User = user;
            ActivateAllButtons();
            userButton.GetComponent<Button>().interactable = false;
        });
    }

    private void ActivateAllButtons()
    {
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }
    
    private void RemoveButtons()
    {
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
