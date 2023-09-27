using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuBehaviour : MonoBehaviour
{
    public Text levelNameText;
    public GameObject lockImage;
    
    private bool _locked;
    private string _levelName;
    private bool _roll;
    private float _angle = 0.01f;

    private void Start()
    {
        var sceneName = gameObject.scene.name;
        
        var regex = new Regex(@"([a-zA-Z]+)(\d+)");

        _levelName = regex.Replace(sceneName, "$1 $2");
    }

    private void Update()
    {
        if (!levelNameText.text.Equals(_levelName))
            levelNameText.text = _levelName;
    }

    [Obsolete("Obsolete")]
    private void FixedUpdate()
    {
        if (!_roll) return;

        transform.RotateAround(new Vector3(0, 0, 1), -_angle);
        _angle += 0.005f;
    }

    public void LevelsMenuButtonClick()
    {
        _locked = true;
        _roll = true;
        lockImage.SetActive(true);

        StartCoroutine(SwitchScene("LevelsMenu"));
    }
    
    public void ReplayButtonClick()
    {
        _locked = true;
        _roll = true;
        lockImage.SetActive(true);

        StartCoroutine(SwitchScene(gameObject.scene.name));
    }

    private IEnumerator SwitchScene(string sceneName)
    {
        yield return new WaitForSeconds(1.5f);
        SceneTransition.SwitchScene(sceneName);
    }

    public bool IsLocked()
    {
        return _locked;
    }
}
