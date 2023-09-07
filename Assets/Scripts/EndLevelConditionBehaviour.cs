using UnityEngine;
using UnityEngine.UI;

public class EndLevelConditionBehaviour : MonoBehaviour
{
    public int requiredScore;
    public Text scoreText;
    public string nextLevel;

    private bool _end;

    private void Update()
    {
        if (_end) return;
        
        
        if (int.Parse(scoreText.text[6..]) >= requiredScore
            )
        {
            _end = true;
            SceneTransition.SwitchScene(nextLevel);
        }
    }
}
