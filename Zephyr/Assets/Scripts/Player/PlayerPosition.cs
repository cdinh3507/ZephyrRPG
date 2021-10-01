using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;


public class PlayerPosition : MonoBehaviour
{
    private CinemachineVirtualCamera CVC;

    private GameManager gm;
  
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        transform.position = gm.lastCheckPointPos;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        }
    }



    
}
