using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

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

    public void TestPee()
    {
        Debug.Log("Pee");
    }

    private void UpdateDialogue()
    {
        dialogueText.text = currentDialogue.dialogueText;
        if(currentDialogue.nextDialogue != null)
        {
            nextDialogue = currentDialogue.nextDialogue; //get the next dialogue from the SO's value
        }
        else
        {
            conversationIsComplete = true; //there was no nextDialogue so convo is done
            onConverstionComplete.Invoke();
        }
        //possibly start a functtion here that "plays" the next dialogue



    }
}
