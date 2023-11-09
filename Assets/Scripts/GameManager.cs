using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public State State = State.MainMenu;

    [SerializeField] int score, lives = 3;
    int actualScore,actualLives;

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }
    
    void Start()
	{
        actualScore = score;
        actualLives = lives;
	}

	public void StartGame()
	{
        actualScore = score;
        actualLives = lives;
        Events.UpdateScore?.Invoke(score, PlayerPrefs.GetInt("bestscore"));
        Events.UpdateLives?.Invoke(lives);
        Events.DisplayMainMenu?.Invoke(false);
        Events.DisplayGameMenu?.Invoke(true);
        State = State.Playing;
	}

    public void ExitGame()
	{
        SceneManager.LoadScene(0); 
	}
    public void GameOver()
	{
        if (State == State.Playing)
		{
            if (actualScore > PlayerPrefs.GetInt("bestscore"))
                PlayerPrefs.SetInt("bestscore", actualScore);
            State = State.MainMenu;
            Events.DisplayGameMenu?.Invoke(false);
            Events.DisplayMainMenu?.Invoke(true);
        }
	}

    public void AddScore(int scoreToAdd)
	{
        actualScore += scoreToAdd;
        Events.UpdateScore?.Invoke(actualScore, PlayerPrefs.GetInt("bestscore"));
	}

    public void RemoveAllLives()
	{
        actualLives = 0;
        Events.UpdateLives?.Invoke(actualLives);
        GameOver();
    }

    public void RemoveLife()
	{
        actualLives--;
        Events.UpdateLives?.Invoke(actualLives);
        if (actualLives == 0) GameOver();
	}
}
