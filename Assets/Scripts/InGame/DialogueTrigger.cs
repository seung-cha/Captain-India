using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class DialogueTrigger : MonoBehaviour
{
   public bool queued;
    public Speech item;

    PlayableDirector director;
    public List<PlayableAsset> asset;
    
    private void Start()
    {
        director = GetComponent<PlayableDirector>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!queued)
        {
            if (director != null && asset != null)
            {
                DialogueManager.Manager.clips = asset;
                DialogueManager.Manager.director = director;
            }
            //Debug 
            DialogueManager.Manager.EnqueueDialogue(item);           
            DialogueManager.Manager.DisplayDialogue();
            queued = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DialogueManager.Manager.HideDialogue();
        PlayerManager.Manager.canMove = true;
    }
}
