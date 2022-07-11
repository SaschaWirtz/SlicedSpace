using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    private Queue<string> sentences;
    public Text nameText;
    public Text dialogueText;

    // Start is called before the first frame update
    void Start()
    {
        this.sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue) {
        FindObjectsOfType<PlayerMovement>()[0].blockPlayerInput = true;
        GameObject.Find("DialogueBox").GetComponent<Image>().enabled = true;
        GameObject.Find("NameText").GetComponent<Text>().enabled = true;
        GameObject.Find("DialogueText").GetComponent<Text>().enabled = true;
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
        this.dialogueText.text = sentence;
    }

    private void EndDialogue() {
        StartCoroutine(this.enablePlayerInput());
        GameObject.Find("DialogueBox").GetComponent<Image>().enabled = false;
        GameObject.Find("NameText").GetComponent<Text>().enabled = false;
        GameObject.Find("DialogueText").GetComponent<Text>().enabled = false;
    }

    private IEnumerator enablePlayerInput() {
        yield return new WaitForSeconds(0.1f);
        FindObjectsOfType<PlayerMovement>()[0].blockPlayerInput = false;
    }
}
