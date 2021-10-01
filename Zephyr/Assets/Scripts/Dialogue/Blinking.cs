using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Blinking : MonoBehaviour
{

    Animator an;
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        an = GetComponent<Animator>();
        active = true;
        StartCoroutine(Blink());
    }

    void OnDisable()
    {
        active = false;
    }

    void OnEnable()
    {
        an = GetComponent<Animator>();
        active = true;
        StartCoroutine(Blink());
    }    
    
    IEnumerator Blink()
    {
        while (active)
        {
            an.SetBool("reset", true);
            Debug.Log("blink");       
            yield return new WaitForSecondsRealtime(Random.Range(4f, 11f));
        }
    }
}
