using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject interactText;
    public bool canInteracte;
    public DialogueBlueprint dialogue;
    private bool isOpen = false;
    [SerializeField] private DialogueManager dialogueManager;
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && canInteracte == true && isOpen == false)
        {
         
            interactText.SetActive(false);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 3)
        {
            interactText.SetActive(true);
            canInteracte = true;
           
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 3)
        {
            interactText.SetActive(false);
           
            dialogueManager.CloseAnim(); 
        }
        canInteracte = false;
    }

    public void open(bool isOpen)
    {
        this.isOpen = isOpen;
    }
}
