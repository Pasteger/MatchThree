using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    private static SceneTransition _sceneTransition;
    
    private static bool _shouldPlayOpenAnimation = true;
    
    private Animator _animator;

    public Text percentText;
    public Image progressBar;

    private float _fakeProgress;
    
    private static string _sceneName;
    
    public static void SwitchScene(string sceneName)
    {
        _sceneTransition._animator.SetTrigger("closeScene");

        _sceneName = sceneName;
        _sceneTransition._fakeProgress = 0;
        _sceneTransition.progressBar.fillAmount = 0;
        _sceneTransition.percentText.text = 0 + "%";
    }

    public void OnAnimationOver()
    {
        _shouldPlayOpenAnimation = true;
    }
    
    private void Start()
    {
        _sceneTransition = this;
        
        _animator = GetComponent<Animator>();

        if (_shouldPlayOpenAnimation) 
        {
            _animator.SetTrigger("openScene");
            _sceneTransition.progressBar.fillAmount = 1;
            
            _shouldPlayOpenAnimation = false; 
        }
    }

    private void Update()
    {
        if (!progressBar.IsActive()) return;
        
        if (_fakeProgress > 1f && progressBar.fillAmount.Equals(1f))
        {
            SceneManager.LoadScene(_sceneName);
            _fakeProgress = 0;
        }
        else
        {
            percentText.text = (int) (_fakeProgress * 100f) + "%";
            progressBar.fillAmount = _fakeProgress;
            _fakeProgress += 0.01f;
        }
    }
}
