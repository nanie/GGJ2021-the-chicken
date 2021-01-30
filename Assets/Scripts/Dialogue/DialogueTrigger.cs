using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool canInteracte; 
    public DialogueBlueprint dialogue;
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canInteracte == true)
        {
            
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 3)
        {
            canInteracte = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 3)
        {
            canInteracte = false;
        }
    }
}
