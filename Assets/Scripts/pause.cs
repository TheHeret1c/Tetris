using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{
    public GameObject panelMenu;
    public GameObject panelGameOver;
    public Button buttonVolume;
    public GameObject buttonExit;
    public GameObject music;
    public GameObject[] gameObjects;
    public Sprite sprite1;
    public Sprite sprite2;
    public static int height = 20;
    public static int width = 10;
    

    public void Pause()
    {
        panelMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Continuegame()
    {
        Time.timeScale = 1f;
        panelMenu.SetActive(false);
    }

    public void GameOver()
    {
        panelGameOver.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        panelGameOver.SetActive(false);
        FindObjectOfType<Move>().ClearGrid();
        gameObjects = GameObject.FindGameObjectsWithTag("Tetromino");
        for (int i = 0; i < gameObjects.Length; ++i)
        {
            Destroy(gameObjects[i]);
        }
        FindObjectOfType<SpawnTetromino>().NewTetromino();
        Time.timeScale = 1f;
    }

    public void Volume()
    {
        if (buttonVolume.tag == "Volume")
        {
            music.GetComponent<AudioSource>().volume = 0;
            buttonVolume.tag = "Mute";
            buttonVolume.GetComponent<Image>().sprite = sprite2;
        }
        else if (buttonVolume.tag == "Mute")
        {
            music.GetComponent<AudioSource>().volume = 1;
            buttonVolume.tag = "Volume";
            buttonVolume.GetComponent<Image>().sprite = sprite1;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
