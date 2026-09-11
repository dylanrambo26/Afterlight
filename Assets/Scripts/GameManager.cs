using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; private set; }

    public int CurrentLevel { get; private set; } = 1;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void GoToNextLevel()
    {
        CurrentLevel++;
        SceneManager.LoadScene(CurrentLevel);
    }

    public void LoadLevel(int level)
    {
        CurrentLevel = level;
        SceneManager.LoadScene(level);
    }
}
