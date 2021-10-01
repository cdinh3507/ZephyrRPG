using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{

    private GameManager gm;
    private Animator anim;

    void Start() {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            gm.lastCheckPointPos = transform.position;
            anim.SetBool("Touched", true);
            gm.SaveGame(); 
        }
    }
}
