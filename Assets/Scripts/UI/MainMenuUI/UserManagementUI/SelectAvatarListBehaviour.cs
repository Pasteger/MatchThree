using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectAvatarListBehaviour : MonoBehaviour
{
    public GameObject selectAvatarButtonPrefab;
    public List<Sprite> avatars;
    
    private string _selectedAvatarName;

    public string GetAvatarName()
    {
        var spriteName = _selectedAvatarName;
        
        _selectedAvatarName = null;
        ActivateAllButtons();

        return spriteName;
    }
    
    private void Start()
    {
        foreach (var avatar in avatars)
        {
            CreateButton(avatar);
        }
    }
    
    private void CreateButton(Sprite avatar)
    {
        var selectAvatarButton = Instantiate(selectAvatarButtonPrefab, transform);
        selectAvatarButton.GetComponent<Image>().sprite = avatar;
        
        selectAvatarButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            _selectedAvatarName = selectAvatarButton.GetComponent<Image>().sprite.name;
            ActivateAllButtons();
            selectAvatarButton.GetComponent<Button>().interactable = false;
        });
    }
    
    private void ActivateAllButtons()
    {
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }
}
