using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    UnityEvent enablePauseEvent, disablePauseEvent;
    
    public string mainMenu;
    public GameObject pauseMenu;
    public CharacterController player;
    public CamController camController;
    public CatsReturnedCounter catsReturnedCounter;

    void Start()
    {
        enablePauseEvent = new UnityEvent();
        disablePauseEvent = new UnityEvent();

        enablePauseEvent.AddListener(EnablePauseMenu);
        disablePauseEvent.AddListener(DisablePauseMenu);

        player.enabled = true;
        camController.enabled = true;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu.activeSelf)
            {
                enablePauseEvent.Invoke();
            }
            else
            {
                disablePauseEvent.Invoke();

            }
        }
    }

    public void EnablePauseMenu()
    {
        if (!pauseMenu.activeSelf)
        {
            player.enabled = false;
            camController.enabled = false;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Confined;
            pauseMenu.SetActive(true);
        }
    }
    public void DisablePauseMenu()
    {
        if (pauseMenu.activeSelf)
        {
            player.enabled = true;
            camController.enabled = true;
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenu.SetActive(false);
        }
    }

    public void SaveGame()
    {
        SaveSystem.Save(catsReturnedCounter);
    }

    public void LoadGame()
    {
       SaveData data = SaveSystem.Load();

        catsReturnedCounter.catsReturned = data.catsReturned;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
