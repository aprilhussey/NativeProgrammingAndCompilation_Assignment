using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
	public Quest quest;

	public TMP_Text nameText;
    public TMP_Text dialogueText;
    
    public GameObject continueButton;

    public Animator animator;
    
    private Queue<string> sentences;

    public GameObject questMenu;
    public GameObject questMenuFirstButton;
    
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
	}

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("open", true);

		// Clear selected button
		EventSystem.current.SetSelectedGameObject(null);
		// Set selected button
		EventSystem.current.SetSelectedGameObject(continueButton);

		nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        //dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
	}

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
		questMenu.SetActive(true);

        Time.timeScale = 0f;

        // Clear selected button
		EventSystem.current.SetSelectedGameObject(null);
		// 
		EventSystem.current.SetSelectedGameObject(questMenuFirstButton);
	}
}
