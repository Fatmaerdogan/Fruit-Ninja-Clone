using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
	[SerializeField] GameObject GameMenu, mainMenu;
    [SerializeField] TextMeshProUGUI scoreText,livesText;


    private void Start()
    {
		Events.UpdateScore += UpdateScore;
        Events.UpdateLives += UpdateLives;
        Events.DisplayMainMenu += DisplayMainMenu;
        Events.DisplayGameMenu += DisplayGameMenu;
    }
    private void OnDestroy()
    {
        Events.UpdateScore -= UpdateScore;
        Events.UpdateLives -= UpdateLives;
        Events.DisplayMainMenu -= DisplayMainMenu;
        Events.DisplayGameMenu -= DisplayGameMenu;
    }
    public void DisplayGameMenu(bool display)=>GameMenu.SetActive(display);

	public void UpdateScore(int newScore, int newBestScore)=>scoreText.text = newScore.ToString()+ "\nBEST : " + newBestScore.ToString();

    public void UpdateLives(int newLives)=>livesText.text = "Lives\n " + newLives.ToString();

	public void DisplayMainMenu(bool display)=>mainMenu.SetActive(display);
}
