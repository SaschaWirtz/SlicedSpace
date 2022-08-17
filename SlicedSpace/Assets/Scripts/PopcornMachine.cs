using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopcornMachine : MonoBehaviour
{

    public Dialogue dialogueNotEnoughButter;
    public Dialogue dialogueEnoughButter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.CompareTag("Player")) {
            this.transition();
        }
    }

    private void transition() {
        if(ScoreManager.instance.requirementCheck()) {
            FindObjectsOfType<DialogueManager>()[0].StartDialogue(this.dialogueEnoughButter, TutorialType.noTutorial);
        }else {
            GameObject.Find("Player").GetComponent<PlayerMovement>().blockSwitch = false;
            GameObject.Find("Canvas").GetComponent<ScoreManager>().blockSwitch = false;
            GameObject.Find("Camera_2D-View").GetComponent<View_controller>().blockSwitch = false;
            FindObjectsOfType<DialogueManager>()[0].StartDialogue(this.dialogueNotEnoughButter, TutorialType.mapTutorial);
        }
    }
}
