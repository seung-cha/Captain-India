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
        queued = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player" || PlayerManager.Manager.hp <= 0) { return; }

        if (!queued)
        {
            if (director != null)
            {
                DialogueManager.Manager.director = this.director;
                
            }
            if(asset != null)
            {
                DialogueManager.Manager.clips = asset;
            }
            
            DialogueManager.Manager.EnqueueDialogue(item);           
            DialogueManager.Manager.DisplayDialogue();
            queued = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player") { return; }

        DialogueManager.Manager.HideDialogue();
        PlayerManager.Manager.canMove = true;
        PlayerManager.Manager.onDialogue = false;
    }
}
