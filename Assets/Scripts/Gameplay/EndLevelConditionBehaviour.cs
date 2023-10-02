using UnityEngine;

public class EndLevelConditionBehaviour : MonoBehaviour
{
    public int requiredScore;

    public GameObject scoreboard;
    
    public GameObject endLevelMenu;

    public string nextLevel;
    
    private UserDataManager _userDataManager;
    private ScoreCalculator _scoreCalculator;
    
    private bool _end;

    private void Awake()
    {
        _userDataManager = UserDataManager.GetInstance();
        _scoreCalculator = GetComponent<ScoreCalculator>();
    }

    private void Update()
    {
        if (_end) return;
        
        if (_scoreCalculator.Score >= requiredScore &&
            _scoreCalculator.meteorites.transform.childCount == 0
            )
        {
            _end = true;
            
            ActiveUser.User.levelCount = int.Parse(nextLevel[5..]);
            
            scoreboard.gameObject.SetActive(false);
            _userDataManager.SaveUsers();
            endLevelMenu.SetActive(true);
        }
    }
}
