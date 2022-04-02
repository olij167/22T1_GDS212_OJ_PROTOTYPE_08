using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuFunctions : MonoBehaviour
{
    public string gameScene;
    public CatsReturnedCounter catsReturnedCounter;

    public TextMeshProUGUI catsToCollectText, currentLevelText;

    public int maxCats = 100000;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;

        catsToCollectText.text = (maxCats - catsReturnedCounter.catsReturned).ToString() + " of them!";
        //currentLevelText.text = "Level " + currentLevel.ToString();
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
