using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Manager;

    public PlayableDirector director;
    public List<PlayableAsset> clips;
    private Queue<Text> dialogue;


    [SerializeField]
    private TextMeshProUGUI dialogueText;
    [SerializeField]
    private RawImage[] images;
    [SerializeField]
    private TextMeshProUGUI[] names;
    [SerializeField]
    private GameObject dialogueBody;
    [SerializeField]
    private GameObject[] speakerBody;
    [SerializeField]
    private Button nextButton;
    [SerializeField]
    private float speed;

    bool stopCoroutine;
    string currentText;
    Queue<int> dialogueValue;

   public int timeLineIndex;
    private void Awake()
    {
        if (Manager == null)
            Manager = this;

        if (Manager != this && Manager != null)
            Destroy(Manager.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        dialogue = new Queue<Text>();
        dialogueValue = new Queue<int>();

        if (dialogueBody.activeSelf == true)
            dialogueBody.SetActive(false);

    }



    public void EnqueueDialogue(Speech speech)
    {
        timeLineIndex = -1;
        dialogueValue.Clear();
        dialogue.Clear();
        foreach (Text item in speech.texts)
            dialogue.Enqueue(item);

        foreach (int value in speech.value)
            dialogueValue.Enqueue(value);

      SetDialogue();


    }

    public void DisplayDialogue()
    {
        dialogueBody.SetActive(true);
    }

    public void HideDialogue()
    {
        dialogueBody.SetActive(false);
    }



    public void SetDialogue()
    {
        

        stopCoroutine = false;
        /*
        bool canProceed;
        canProceed = dialogue.Count -1 == 0 ? false : true;
        nextButton.interactable = canProceed;
        */

        PlayerManager.Manager.canMove = false;

        timeLineIndex++;

        if (clips.Count > timeLineIndex && timeLineIndex >= 0)
        {
            if (clips.ElementAt(timeLineIndex) != null)
            {
                director.playableAsset = clips.ElementAt(timeLineIndex);
                director.Play();
            }
        }

        if (dialogue.Count == 0)
        { return; }

        Text temp = dialogue.Peek();
        dialogue.Dequeue();

        StartCoroutine(AnimateDialogue(temp.text));

        for(int i = 0; i < images.Length; i++)
        {
            images[i].texture = temp.image;
            names[i].text = temp.speaker;
        }

     
   
        if (dialogueValue.Peek() == 0)
        {
            // leftSide;
            dialogueText.alignment = TextAlignmentOptions.TopLeft;
            speakerBody[0].SetActive(true);
            speakerBody[1].SetActive(false);
        }
        else
        {
            //rightSide;
            dialogueText.alignment = TextAlignmentOptions.TopRight;
            speakerBody[1].SetActive(true);
            speakerBody[0].SetActive(false);
        }      
        dialogueValue.Dequeue();
    }

    private IEnumerator AnimateDialogue(string text)
    {
        currentText = text;
       char[] characters = text.ToCharArray();
        string temp = null;
        foreach(char chars in characters)
        {

            if (stopCoroutine)
                break;

            temp += chars;
            dialogueText.text = temp;
            yield return new WaitForSeconds(speed);
        }
    }


    public void Proceed()
    {
        
        if(dialogueText.text != currentText)
        {
            // Stop coroutine
            stopCoroutine = true;
            dialogueText.text = currentText;
        }
        else if(dialogueText.text == currentText && dialogue.Count != 0)
        {          
            SetDialogue();
        }
        else
        {
            // Exit
            timeLineIndex = -1;
            PlayerManager.Manager.canMove = true;
            dialogueBody.SetActive(false);
           
        }

    }



}
