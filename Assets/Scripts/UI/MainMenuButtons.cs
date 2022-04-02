using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public string gameScene;
    public CatsReturnedCounter catsReturnedCounter;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void SaveGame()
    {
        SaveSystem.Save(catsReturnedCounter);
    }

    public void LoadGame()
    {
        SaveData data = SaveSystem.Load();

        catsReturnedCounter.catsReturned = data.catsReturned;
        SceneManager.LoadScene(gameScene);
    }

    public void NewGame()
    {
        catsReturnedCounter.catsReturned = 0;
        SceneManager.LoadScene(gameScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
