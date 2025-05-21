using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIMenu : MonoBehaviour
{
    public void PlayGame(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
    public void Retry()
    {
        SceneManager.LoadScene("Dzaky");
    }

    public void EnableMenu(GameObject menu)
    {
        menu.SetActive(true);
    }
    public void DisableMenu(GameObject menu)
    {
        menu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
