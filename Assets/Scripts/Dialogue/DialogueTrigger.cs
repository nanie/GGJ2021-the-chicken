using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool canInteracte; 
    public DialogueBlueprint dialogue;
    private bool isOpen = false;
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canInteracte == true && isOpen == false)
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

    public void open(bool isOpen)
    {
        this.isOpen = isOpen;
    }
}
