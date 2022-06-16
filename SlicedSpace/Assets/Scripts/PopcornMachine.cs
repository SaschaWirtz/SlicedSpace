using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopcornMachine : MonoBehaviour
{
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
            // Load next scene
        }else {
            // Message that not enough butter was collected
        }
    }
}
