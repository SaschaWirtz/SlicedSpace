using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
	public Dialogue dialogue;
    private bool played = false;

    private void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.CompareTag("Player") && !this.played) {
            FindObjectsOfType<DialogueManager>()[0].StartDialogue(this.dialogue);
            this.played = true;
        }
    }
}
