using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBehavior : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject gameCanvas;
    public GameObject tutorialCanvas;
    public GameObject gameOverCanvas;

    public AudioClip storeBGM;
    public AudioClip menuBGM;
    public AudioClip gameOverBGM;

    public AudioClip selectNoise;

    private float gameOverTimer = 3.0f;

    public void StartGame()
	{
        mainMenuCanvas.SetActive(false);
        gameCanvas.SetActive(true);

        AudioBehavior.Instance.PlaySound(selectNoise);
        AudioBehavior.Instance.ChangeBGM(storeBGM);
	}
    public void EnableTutorial()
	{
        AudioBehavior.Instance.PlaySound(selectNoise);

        tutorialCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
	}
    public void DisableTutorial()
	{
        AudioBehavior.Instance.PlaySound(selectNoise);

        tutorialCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
	}
    public void GameOver()
	{
        gameOverCanvas.SetActive(true);
        gameCanvas.SetActive(false);
        AudioBehavior.Instance.ChangeBGM(gameOverBGM);
        Invoke("BackToMenu", gameOverTimer);
	}
    private void BackToMenu()
	{
        gameOverCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);

        AudioBehavior.Instance.ChangeBGM(menuBGM);
	}
    public void ExitGame()
	{
        Application.Quit();
	}
}
