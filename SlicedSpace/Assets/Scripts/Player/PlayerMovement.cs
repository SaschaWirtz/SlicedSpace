using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float jumpHight = 400f;
    private Rigidbody rb;
    public float speed = 5.0f;
    private float horizontalInput;
    private bool jumpPressed = false;
    private bool isOnGround = true;
    private bool isSwitched = false;
    private int butterAmount;

    // Start is called before the first frame update
    void Start()
    {
        butterAmount = 0;
        rb = this.GetComponent<Rigidbody>();
        View_controller.OnViewChanged += switchOrientation;
    }

    // Update is called once per frame
    void Update()
    {     
        if(Input.GetButtonDown("Jump") && isOnGround) {
            rb.drag = 1;
            rb.mass = 1;
            rb.AddForce(Vector3.up * jumpHight);
            isOnGround = false;
        }else if(Input.GetButtonUp("Jump")) {
            rb.drag = 1;
            rb.mass = 1;
            //rb.AddForce (0,10000,0);
        }
        horizontalInput = Input.GetAxis("Horizontal");
        if(isSwitched) {
            transform.Translate(Vector3.back * Time.deltaTime * speed * horizontalInput);
        }else {
            transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        }

        if(Input.GetKeyDown(KeyCode.C)) 
        {
            Debug.Log("You have " + butterAmount + " x Butter");
        }
        
    }
    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Ground")){
            isOnGround = true;
        }
    }
    private void switchOrientation() {
        isSwitched = !isSwitched;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag=="Butter") 
        {
            butterAmount++;
            other.gameObject.SetActive(false);
            ScoreManager.instance.addPoint();
        }
    }
}
