using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMap : MonoBehaviour
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
            GameObject.Find("Player").GetComponent<PlayerMovement>().blockSwitch = false;
            GameObject.Find("Canvas").GetComponent<ScoreManager>().blockSwitch = false;
            GameObject.Find("Camera_2D-View").GetComponent<View_controller>().blockSwitch = false;
        }
    }
}
