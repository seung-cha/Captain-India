using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideDIalogue : MonoBehaviour
{
    public void Hide()
    {
        DialogueManager.Manager.HideDialogue();
    }
}
