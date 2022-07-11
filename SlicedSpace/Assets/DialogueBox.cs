using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("DialogueBox").GetComponent<Image>().enabled == true && Input.anyKeyDown) {
            FindObjectsOfType<DialogueManager>()[0].DisplayNextSentence();
        }
    }
}
