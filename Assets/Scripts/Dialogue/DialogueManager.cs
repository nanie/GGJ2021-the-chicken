using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public Text nameText;
    Queue<string> sentences;
    public Animator animator;
    public bool isOpen;
    
    // Start is called before the first frame update
    void Start()
    {
       
        sentences = new Queue<string>();
    }

    public void StartDialogue(DialogueBlueprint dialogue)
    {
        isOpen = true;
        OpenAnim();
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        if (sentences.Count > 0)
        {
            DisplayOnScreen();
        }
    }

    public void OpenAnim()
    {
        if (isOpen)
        {
            animator.SetBool("open", true);
        }
    }

    public void CloseAnim()
    {
        isOpen = false;
        if (!isOpen)
        {
            animator.SetBool("open", false);
        }
    }
    
    public void DisplayOnScreen()
    {
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        //dialogueText.text = sentence;
    }

    void EndDialogue()
    {

    }

    // Update is called once per frame
    
}
