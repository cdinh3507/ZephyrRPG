using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    private const int X_COORD = 0;
    private const int Y_COORD = 1; 

    private static GameManager instance;
    public bool load = false;
    public Vector2 lastCheckPointPos;
    public Vector2 startingPoint;


    //[SerializeField]
    //private GameObject player;
    [SerializeField]
    private float respawnTime;

    private float respawnTimeStart;

    private bool respawn;

    private CinemachineVirtualCamera CVC;


    void Awake() {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
             
        }
        else
        {
            Destroy(gameObject);
        }
       
    }

    private void Update()
    {
        CheckRespawn();
    }

    public void Respawn()
    {
        respawnTimeStart = Time.time;
        respawn = true;
    }

    private void CheckRespawn()
    {
        if(Time.time >= respawnTimeStart + respawnTime && respawn)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            respawn = false;
        }
    }

    public void SaveGame() {
        SaveSystem.SaveGame(this);
    }

    public void resetStartingPoint()
    {
        lastCheckPointPos.x = startingPoint.x;
        lastCheckPointPos.y = startingPoint.y;

    }

    public void LoadGame() {
       PlayerData data = SaveSystem.LoadGame();

        if (data != null)
        {
            lastCheckPointPos.x = data.position[X_COORD];
            lastCheckPointPos.y = data.position[Y_COORD];
        }

    }

}
