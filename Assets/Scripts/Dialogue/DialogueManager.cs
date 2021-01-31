using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    Queue<string> sentences;
    public Animator animator;
    public bool isOpen;
    public DialogueTrigger dialoguetrigger;
    // Start is called before the first frame update
    void Start()
    {

        sentences = new Queue<string>();
    }

    private void Update()
    {
        if (!isOpen)
            return;

        if (Input.GetButtonDown("Fire1") && sentences.Count > 0)
        {            
            DisplayOnScreen();
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            CloseAnim();
        }
        else if (sentences.Count <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                StartDialogue(dialoguetrigger.dialogue);
            }
        }


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
        animator.SetBool("open", true);
        isOpen = true;
        dialoguetrigger.open(isOpen);
    }

    public void CloseAnim()
    {
        isOpen = false;
        animator.SetBool("open", false);
        dialoguetrigger.open(isOpen);
    }

    public void DisplayOnScreen()
    {
        
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));


        //dialogueText.text = sentence;
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char character in sentence.ToCharArray())
        {
            
            dialogueText.text += character;
            yield return null;
        }

        //dialogueText.text = sentence;
    }

    void EndDialogue()
    {

    }

    // Update is called once per frame

}
