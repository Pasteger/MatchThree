using UnityEngine;
using UnityEngine.UI;

public class EndLevelConditionBehaviour : MonoBehaviour
{
    public int requiredScore;
    public Text scoreText;
    public Text endLevelMenuScoreText;
    public string nextLevel;
    public GameObject endLevelMenu;
    
    private UserDataManager _userDataManager;
    
    private bool _end;

    private void Awake()
    {
        _userDataManager = UserDataManager.GetInstance();
    }

    private void Update()
    {
        if (_end) return;
        
        
        if (int.Parse(scoreText.text[6..]) >= requiredScore
            )
        {
            _end = true;
            
            ActiveUser.User.levelCount = int.Parse(nextLevel[5..]);
            
            scoreText.gameObject.SetActive(false);
            _userDataManager.SaveUsers();
            endLevelMenu.SetActive(true);
            endLevelMenuScoreText.text = scoreText.text;
        }
    }
}
