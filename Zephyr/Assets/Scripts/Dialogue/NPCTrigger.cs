using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{

    DialogueTrigger dt;
    Animator an;
    public Item it;
    public DialogueManager dialogueManager;
    private bool touchPlayer;
    private bool rightDirection;

    private float BUFFER = .5f;

    //public Player2DController player;
    private GameObject player;

  


    // Start is called before the first frame update
    void Start()
    {
        dt = GetComponent<DialogueTrigger>();
        an = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && touchPlayer && !dialogueManager.isDisplaying)
        {        
            //make sure player is facing right direction
            if (player != null)
            {
                //get differences of x and y pos
                float difX = player.transform.Find("Pivot").position.x - gameObject.transform.Find("Pivot").position.x;
                float difY = player.transform.Find("Pivot").position.y - gameObject.transform.Find("Pivot").position.y;
                Animator playerAnimator = player.GetComponent<Animator>();
                //left of npc
                if ((Mathf.Abs(difX) > Mathf.Abs(difY) || Mathf.Abs(difY) - Mathf.Abs(difX) < BUFFER) 
                    && difX < 0 && playerAnimator.GetFloat("HorizontalPos") == 1)
                {
                    rightDirection = true;
                    if (an != null)
                    {
                        an.SetFloat("Horizontal", -1);
                        an.SetFloat("Vertical", 0);
                    }
                    
                } else
                //right of npc
                if ((Mathf.Abs(difX) > Mathf.Abs(difY) || Mathf.Abs(difY) - Mathf.Abs(difX) < BUFFER) 
                    && difX >= 0 && playerAnimator.GetFloat("HorizontalPos") == -1)
                {
                    rightDirection = true;
                    if (an != null)
                    {
                        an.SetFloat("Horizontal", 1);
                        an.SetFloat("Vertical", 0);
                    }
                }
                else
                //up of npc
                if ((Mathf.Abs(difX) < Mathf.Abs(difY) || Mathf.Abs(difX) - Mathf.Abs(difY) < BUFFER) 
                    && difY >= 0 && playerAnimator.GetFloat("VerticalPos") == -1)
                {
                    rightDirection = true;
                    if (an != null)
                    {
                        an.SetFloat("Horizontal", 0);
                        an.SetFloat("Vertical", 1);
                    }
                }
                else
                //down of npc
                if ((Mathf.Abs(difX) < Mathf.Abs(difY) || Mathf.Abs(difX) - Mathf.Abs(difY) < BUFFER) 
                    && difY < 0 && playerAnimator.GetFloat("VerticalPos") == 1)
                {
                    rightDirection = true;
                    if (an != null)
                    {
                        an.SetFloat("Horizontal", 0);
                        an.SetFloat("Vertical", -1);
                    }
                }
                else
                {
                    rightDirection = false;
                }
            }

            if (rightDirection)
            {
                Debug.Log("trigger");
                if (dt != null)
                {
                    dt.TriggerDialogue();
                } else
                if (it != null)
                {
                    Debug.Log("item");
                    Destroy(gameObject);
                }
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        touchPlayer = true;
        player = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        touchPlayer = false;
    }

}
