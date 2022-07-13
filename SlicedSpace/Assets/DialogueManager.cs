using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TutorialType{
    noTutorial,
    movementTutorial,
    mapTutorial
}

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    private TutorialType tutorialType = TutorialType.noTutorial;

    // Start is called before the first frame update
    void Start()
    {
        this.sentences = new Queue<string>();
    }

    void Update() {
        if(Input.anyKeyDown) {
            GameObject.Find("Panel").GetComponent<Image>().enabled = false;
            GameObject.Find("Movement").GetComponent<Image>().enabled = false;
            GameObject.Find("Map").GetComponent<Image>().enabled = false;
        }
    }

    public void StartDialogue(Dialogue dialogue, TutorialType tutorialType) {
        FindObjectsOfType<PlayerMovement>()[0].blockPlayerInput = true;
        this.tutorialType = tutorialType;
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
        this.animator.SetBool("IsOpen", false);
        switch (this.tutorialType)
        {
            case TutorialType.movementTutorial:
                GameObject.Find("Panel").GetComponent<Image>().enabled = true;
                GameObject.Find("Movement").GetComponent<Image>().enabled = true;
                break;
            case TutorialType.mapTutorial:
                GameObject.Find("Panel").GetComponent<Image>().enabled = true;
                GameObject.Find("Map").GetComponent<Image>().enabled = true;
                break;
            default:
            break;
        }
        this.tutorialType = TutorialType.noTutorial;
        StartCoroutine(this.enablePlayerInput());

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
