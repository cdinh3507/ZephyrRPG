using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{

    public string newGameScene;
    public string loadGameScene;
    private GameManager gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    public void NewGame() {
        SceneManager.LoadScene(newGameScene);
        gm.resetStartingPoint();
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void LoadGame() {
        SceneManager.LoadScene(newGameScene);
        gm.LoadGame();

    }
}
