using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    private Queue<string> sentences;
    public Text nameText;
    public Text dialogueText;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) {
        FindObjectsOfType<PlayerMovement>()[0].blockPlayerInput = true;
        this.animator.SetBool("IsOpen", true);

        this.nameText.text = dialogue.name;

        this.sentences.Clear();

        foreach(string sentence in dialogue.sentences) {
            this.sentences.Enqueue(sentence);
        } 

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if(this.sentences.Count == 0) {
            EndDialogue();
            
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private void EndDialogue() {
        StartCoroutine(this.enablePlayerInput());
        this.animator.SetBool("IsOpen", false);
    }

    private IEnumerator enablePlayerInput() {
        yield return null;
        FindObjectsOfType<PlayerMovement>()[0].blockPlayerInput = false;
    }

    private IEnumerator TypeSentence (string sentence) {
        this.dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray()) {
            this.dialogueText.text += letter;
            yield return null;
        }
    }
}
