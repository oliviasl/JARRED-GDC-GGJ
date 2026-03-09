using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "Scriptable Objects/DialogueSO")]
public class DialogueSO : ScriptableObject
{
   [TextArea(3, 10)] public string dialogueText;
   [SerializeField] public DialogueSO nextDialogue;
   [SerializeField] public bool isInteractable;
   [SerializeField] public int dialogueID;
}
