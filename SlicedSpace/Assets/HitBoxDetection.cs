using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxDetection : MonoBehaviour
{

    private bool isHit = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void reset() {
        this.isHit = false;
    }

    public bool IsHit() {
        return this.isHit;
    }

    private void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.CompareTag("Player")) {
            this.isHit = true;
        }
    }

    public void destroy() {
        Destroy(this);
    }
}
