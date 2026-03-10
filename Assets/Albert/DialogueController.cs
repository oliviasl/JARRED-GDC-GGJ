using Febucci.TextAnimatorForUnity;
using Febucci.TextAnimatorForUnity.Actions;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[System.Serializable]
public class DialogueEvent
{
    public int dialogueID;
    public UnityEvent onDialogueReached;
}
public class DialogueController : MonoBehaviour
{
    [Header("DialogueProgression")]
    [SerializeField] private bool dialogueIsComplete; //specfic speech bubble is donee
    [SerializeField] private DialogueSO currentDialogue;
    [SerializeField] private DialogueSO nextDialogue;
    [SerializeField] private bool conversationIsComplete; //the entire interaction is complete
    [SerializeField] private UnityEvent onConverstionComplete;
    [Header("TMP Text")]
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TypewriterComponent typewriter;
    [Header("DialogueEvents")]
    [SerializeField] private DialogueEvent[] dialogueEvents;
    [Header("Inputs")]
    [SerializeField] private CharacterInput characterInput;

    
    private void Start()
    {
        UpdateDialogue();
    }
    private void Update()
    {
        
    }

    public void InteractWithDialogue()
    {
        if(currentDialogue.isInteractable)
        {
            if (!dialogueIsComplete)
            {
                dialogueIsComplete = true;

                //speed up dialogue/cut to end of it
            }
            if (dialogueIsComplete)
            {
                currentDialogue = nextDialogue;

                UpdateDialogue();
            }
        }
        else
        {
            Debug.Log("Sorry this dialogue is not interactable!");
        }
       

    }

    public void LetDialoguePass(int time)
    {
        StartCoroutine(WaitToLetDialoguePass(time));
    }

    IEnumerator WaitToLetDialoguePass(int time)
    {
        yield return new WaitForSeconds(time);
        typewriter.ShowText(currentDialogue.dialogueText);
        UpdateDialogue();
    }

    public void TestPee()
    {
        Debug.Log("Pee");
    }

    private void UpdateDialogue()
    {
        typewriter.ShowText(currentDialogue.dialogueText);
        foreach (var dialogueEvent in dialogueEvents)
        {
            if(dialogueEvent.dialogueID == currentDialogue.dialogueID)
            {
                dialogueEvent.onDialogueReached.Invoke();
            }
        }
        if(currentDialogue.nextDialogue != null)
        {
            nextDialogue = currentDialogue.nextDialogue; //get the next dialogue from the SO's value
        }
        else
        {
            conversationIsComplete = true; //there was no nextDialogue so convo is done
            StartCoroutine(WaitToPerform(5));
        }
        //possibly start a functtion here that "plays" the next dialogue



    }

    IEnumerator WaitToPerform(int time)
    {
        yield return new WaitForSeconds(time);
        onConverstionComplete.Invoke();
    }
    
}
