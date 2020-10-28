using System;
using UnityEngine;

public class DialogueBehaviour : MonoBehaviour
{
    public StringListData dialogue;
    public HudPrintStringBehaviour textObj;
    
    private bool canStartDialogue;

    private void Update()
    {
        if (!canStartDialogue) return;

        if (!Input.GetButtonDown("Interact")) return;
        
        textObj.DisplayString(dialogue.GetNextString());
    }

    public void WaitForDialogue()
    {
        canStartDialogue = true;
    }

    public void CancelWaitForDialogue()
    {
        canStartDialogue = false;
    }
}
