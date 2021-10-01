using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor.Animations;



public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public GameObject nameBox;
    public GameObject portraitLeft1;
    public GameObject portraitLeft2;
    public GameObject portraitLeft3;
    public GameObject portraitLeft4;
    public GameObject portraitRight1;
    public GameObject portraitRight2;
    public GameObject portraitRight3;
    public GameObject portraitRight4;

    public Animator animator;
    public Animator playerAnimator;
    public Player2DController playerControls;

    private Queue<Sentence> sentences;
    
    private Speaker[] speakerLeft;
    private Image[] mouthLeftImage;
    private Image[] eyeLeftImage;
    private Animator[] mouthLeftAn;
    private Animator[] eyeLeftAn;
    private Blinking[] blinkingLeft;

    private Speaker[] speakerRight;
    private Image[] mouthRightImage;
    private Image[] eyeRightImage;
    private Animator[] mouthRightAn;
    private Animator[] eyeRightAn;
    private Blinking[] blinkingRight;

    private bool hasPortraits;

    const float DEFAULTTYPINGSPEED = 0.05f;

    public float typingSpeed = DEFAULTTYPINGSPEED;

    private bool isTyping;
    public bool isDisplaying = false;
    private bool firstPress = true;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<Sentence>();
        isTyping = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (firstPress)
            {
                firstPress = false;
            } else
            {
                if (isTyping)
                {
                    typingSpeed = 0;
                }
                else
                {
                    DisplayNextSentence();
                }
            }
        }
    }

    public void StartDialogue (Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        isDisplaying = true;
        firstPress = true;
        speakerLeft = dialogue.speakerLeft;
        speakerRight = dialogue.speakerRight;
        hasPortraits = dialogue.hasPortraits;

        //if no speakers then disable name box
        if (dialogue.speakerLeft.Length == 0)
        {
            nameBox.SetActive(false);
        } else
        {
            nameBox.SetActive(true);
        }

        //get portrait images and animators
        if (hasPortraits)
        {
            //LEFT
            mouthLeftImage = new Image[4];
            eyeLeftImage = new Image[4];
            mouthLeftAn = new Animator[4];
            eyeLeftAn = new Animator[4];
            blinkingLeft = new Blinking[4];

            SetPortrait(portraitLeft1, true, 0);
            SetPortrait(portraitLeft2, true, 1);
            SetPortrait(portraitLeft3, true, 2);
            SetPortrait(portraitLeft4, true, 3);

            //reset portraits and set animators
            portraitLeft1.GetComponent<Image>().sprite = dialogue.speakerLeft[0].portraits[0];
            eyeLeftAn[0].runtimeAnimatorController = speakerLeft[0].eyeController;
            mouthLeftAn[0].runtimeAnimatorController = speakerLeft[0].mouthController;
            if (speakerLeft.Length > 1)
            {
                portraitLeft2.GetComponent<Image>().sprite = dialogue.speakerLeft[1].portraits[0];
                eyeLeftAn[1].runtimeAnimatorController = speakerLeft[1].eyeController;
                mouthLeftAn[1].runtimeAnimatorController = speakerLeft[1].mouthController;
            }
            if (speakerLeft.Length > 2)
            {
                portraitLeft3.GetComponent<Image>().sprite = dialogue.speakerLeft[2].portraits[0];
                eyeLeftAn[2].runtimeAnimatorController = speakerLeft[2].eyeController;
                mouthLeftAn[2].runtimeAnimatorController = speakerLeft[2].mouthController;
            }
            if (speakerLeft.Length > 3)
            {
                portraitLeft4.GetComponent<Image>().sprite = dialogue.speakerLeft[3].portraits[0];
                eyeLeftAn[3].runtimeAnimatorController = speakerLeft[3].eyeController;
                mouthLeftAn[3].runtimeAnimatorController = speakerLeft[3].mouthController;
            }

            foreach (Animator an in mouthLeftAn)
            {
                an.SetInteger("index", 0);
            }
            foreach (Animator an in eyeLeftAn)
            {
                an.SetInteger("index", 0);
            }

            //RIGHT
            mouthRightImage = new Image[4];
            eyeRightImage = new Image[4];
            mouthRightAn = new Animator[4];
            eyeRightAn = new Animator[4];
            blinkingRight = new Blinking[4];

            SetPortrait(portraitRight1, false, 0);
            SetPortrait(portraitRight2, false, 1);
            SetPortrait(portraitRight3, false, 2);
            SetPortrait(portraitRight4, false, 3);

            //reset portraits and set animators
            portraitRight1.GetComponent<Image>().sprite = dialogue.speakerRight[0].portraits[0];
            eyeRightAn[0].runtimeAnimatorController = speakerRight[0].eyeController;
            mouthRightAn[0].runtimeAnimatorController = speakerRight[0].mouthController;
            if (speakerRight.Length > 1)
            {
                portraitRight2.GetComponent<Image>().sprite = dialogue.speakerRight[1].portraits[0];
                eyeRightAn[1].runtimeAnimatorController = speakerRight[1].eyeController;
                mouthRightAn[1].runtimeAnimatorController = speakerRight[1].mouthController;
            }
            if (speakerRight.Length > 2)
            {
                portraitRight3.GetComponent<Image>().sprite = dialogue.speakerRight[2].portraits[0];
                eyeRightAn[2].runtimeAnimatorController = speakerRight[2].eyeController;
                mouthRightAn[2].runtimeAnimatorController = speakerRight[2].mouthController;
            }
            if (speakerRight.Length > 3)
            {
                portraitRight4.GetComponent<Image>().sprite = dialogue.speakerRight[3].portraits[0];
                eyeRightAn[3].runtimeAnimatorController = speakerRight[3].eyeController;
                mouthRightAn[3].runtimeAnimatorController = speakerRight[3].mouthController;
            }

            foreach (Animator an in mouthRightAn)
            {
                an.SetInteger("index", 0);
            }
            foreach (Animator an in eyeRightAn)
            {
                an.SetInteger("index", 0);
            }

        }

        sentences.Clear();

 
        foreach (Sentence sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        playerControls.enabled = false;

        if (hasPortraits)
        {
            foreach (Blinking b in blinkingLeft)
            {
                b.enabled = true;
            }

            foreach (Blinking b in blinkingRight)
            {
                b.enabled = true;
            }
        }

        playerAnimator.SetBool("cutscene", true);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        Sentence sentence = sentences.Dequeue();
        StopAllCoroutines();

        
        if (speakerLeft.Length != 0)
        {
            //change name
            nameText.text = sentence.speaker.name;

            if (hasPortraits)
            {
                portraitLeft1.SetActive(true);
                if (speakerLeft.Length > 1)
                {
                    portraitLeft2.SetActive(true);
                }
                if (speakerLeft.Length > 2)
                {
                    portraitLeft3.SetActive(true);
                }
                if (speakerLeft.Length > 3)
                {
                    portraitLeft4.SetActive(true);
                }

                portraitRight1.SetActive(true);
                if (speakerRight.Length > 1)
                {
                    portraitRight2.SetActive(true);
                }
                if (speakerRight.Length > 2)
                {
                    portraitRight3.SetActive(true);
                }
                if (speakerRight.Length > 3)
                {
                    portraitRight4.SetActive(true);
                }

                //find speaker
                Speaker currSpeaker;
                int speakerIndex = 100;
                
                bool isALeftSpeaker = false;
                //search left speakers
                int i = 0;
                foreach (Speaker speaker in speakerLeft)
                {
                    if (sentence.speaker == speaker)
                    {
                        currSpeaker = speaker;
                        isALeftSpeaker = true;
                        speakerIndex = i;
                    }
                    i++;
                }
                if (!isALeftSpeaker)
                {
                    i = 0;
                    foreach (Speaker speaker in speakerRight)
                    {
                        if (sentence.speaker == speaker)
                        {
                            currSpeaker = speaker;
                            speakerIndex = i;
                        }
                        i++;
                    }
                }

                //change portrait
                if (isALeftSpeaker)
                {
                    if (speakerIndex == 0)
                    {
                        portraitLeft1.GetComponent<Image>().sprite = sentence.speaker.portraits[sentence.portraitIndex];
                    } else
                    if (speakerIndex == 1)
                    {
                        portraitLeft2.GetComponent<Image>().sprite = sentence.speaker.portraits[sentence.portraitIndex];
                    } else
                    if (speakerIndex == 2)
                    {
                        portraitLeft3.GetComponent<Image>().sprite = sentence.speaker.portraits[sentence.portraitIndex];
                    }
                    else
                    if (speakerIndex == 3)
                    {
                        portraitLeft4.GetComponent<Image>().sprite = sentence.speaker.portraits[sentence.portraitIndex];
                    }

                    //set indices for mouth and eyes animator
                    mouthLeftAn[speakerIndex].SetInteger("index", sentence.portraitIndex);
                    mouthLeftAn[speakerIndex].SetBool("active", true);
             
                    eyeLeftAn[speakerIndex].SetInteger("index", sentence.portraitIndex);

                    mouthLeftImage[speakerIndex].enabled = true;

                }
                else
                {
                    if (speakerIndex == 0)
                    {
                        portraitRight1.GetComponent<Image>().sprite = sentence.speaker.portraits[sentence.portraitIndex];
                    }
                    else
                    if (speakerIndex == 1)
                    {
                        portraitRight2.GetComponent<Image>().sprite = sentence.speaker.portraits[sentence.portraitIndex];
                    }
                    else
                    if (speakerIndex == 2)
                    {
                        portraitRight3.GetComponent<Image>().sprite = sentence.speaker.portraits[sentence.portraitIndex];
                    }
                    else
                    if (speakerIndex == 3)
                    {
                        portraitRight4.GetComponent<Image>().sprite = sentence.speaker.portraits[sentence.portraitIndex];
                    }

                    //set indices for mouth and eyes animator
                    mouthRightAn[speakerIndex].SetInteger("index", sentence.portraitIndex);
                    mouthRightAn[speakerIndex].SetBool("active", true);

                    eyeRightAn[speakerIndex].SetInteger("index", sentence.portraitIndex);

                    mouthRightImage[speakerIndex].enabled = true;

                }
            } else
            {
                portraitLeft1.SetActive(false);
                portraitLeft2.SetActive(false);
                portraitLeft3.SetActive(false);
                portraitLeft4.SetActive(false);

                portraitRight1.SetActive(false);
                portraitRight2.SetActive(false);
                portraitRight3.SetActive(false);
                portraitRight4.SetActive(false);
            }
        }
        else
        {
            portraitLeft1.SetActive(false);
            portraitLeft2.SetActive(false);
            portraitLeft3.SetActive(false);
            portraitLeft4.SetActive(false);

            portraitRight1.SetActive(false);
            portraitRight2.SetActive(false);
            portraitRight3.SetActive(false);
            portraitRight4.SetActive(false);
        }
        
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (Sentence sentence)
    {
        dialogueText.text = "";
        typingSpeed = DEFAULTTYPINGSPEED;
        
        foreach (char letter in sentence.sentText.ToCharArray())
        {
            isTyping = true;
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
        if (hasPortraits)
        {
            //disable everthing after typing over
            foreach (Image im in mouthLeftImage)
            {
                im.enabled = false;
            }
            foreach (Animator an in mouthLeftAn)
            {
                an.SetBool("active", false);
            }
            foreach (Image im in mouthRightImage)
            {
                im.enabled = false;
            }
            foreach (Animator an in mouthRightAn)
            {
                an.SetBool("active", false);
            }
        }
        isTyping = false;
    } 

    void SetPortrait(GameObject portrait, bool left, int index)
    {
        if (left)
        {
            eyeLeftImage[index] = portrait.transform.Find("eyes").GetComponent<Image>();
            mouthLeftImage[index] = portrait.transform.Find("mouth").GetComponent<Image>();
            eyeLeftAn[index] = portrait.transform.Find("eyes").GetComponent<Animator>();
            mouthLeftAn[index] = portrait.transform.Find("mouth").GetComponent<Animator>();
            blinkingLeft[index] = portrait.transform.Find("eyes").GetComponent<Blinking>();
        } else
        {
            eyeRightImage[index] = portrait.transform.Find("eyes").GetComponent<Image>();
            mouthRightImage[index] = portrait.transform.Find("mouth").GetComponent<Image>();
            eyeRightAn[index] = portrait.transform.Find("eyes").GetComponent<Animator>();
            mouthRightAn[index] = portrait.transform.Find("mouth").GetComponent<Animator>();
            blinkingRight[index] = portrait.transform.Find("eyes").GetComponent<Blinking>();
        }

    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        isDisplaying = false;
        playerControls.enabled = true;
        if (hasPortraits)
        {
            foreach (Blinking b in blinkingLeft)
            {
                b.enabled = false;
            }
            foreach (Blinking b in blinkingRight)
            {
                b.enabled = false;
            }
        }
        playerAnimator.SetBool("cutscene", false);
    }

}
