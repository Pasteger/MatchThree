using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelMenuBehaviour : MonoBehaviour
{
    public Image background;
    public Text levelNameText;
    public string nextLevel;
    
    public ScoreCalculator scoreCalculator;
    
    public Text scoreText;
    public Text meteoriteText;
    
    private string _levelName;
    
    private void Start()
    {
        var sceneName = gameObject.scene.name;
        
        var regex = new Regex(@"([a-zA-Z]+)(\d+)");

        _levelName = regex.Replace(sceneName, "$1 $2");
        
        scoreText.text = scoreCalculator.Score.ToString();
        meteoriteText.text = scoreCalculator.Meteorites.ToString();
    }

    private void Update()
    {
        if (!levelNameText.text.Equals(_levelName))
            levelNameText.text = _levelName;
    }

    [Obsolete("Obsolete")]
    private void FixedUpdate()
    {
        if (!background.IsActive()) return;

        background.rectTransform.RotateAround(new Vector3(0, 0, 1), -0.0005f);
    }

    public void NextLevelButtonClick()
    {
        SceneTransition.SwitchScene(nextLevel);
    }
    public void ReplayButtonClick()
    {
        SceneTransition.SwitchScene(gameObject.scene.name);
    }
    public void LevelsMenuButtonClick()
    {
        SceneTransition.SwitchScene("LevelsMenu");
    }
}
