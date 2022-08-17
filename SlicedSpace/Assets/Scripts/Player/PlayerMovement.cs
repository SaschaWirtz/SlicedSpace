using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float jumpHight = 400f;
    private Rigidbody rb;
    public float speed = 5.0f;
    private float horizontalInput;
    public bool isOnGround = true;
    private bool isSwitched = false;
    private int butterAmount;
    private Vector3 startPosition;
    private int layer2D;
    private int layerGeneral;
    public float planeThickness = 0.5f;
    public float planeDimensionsHeight = 10000f;
    public float planeDimensionsWidth = 10000f;
    private Animator animator;
    private bool isPositiveMovement = true;
    public bool blockSwitch = false;
    public bool blockPlayerInput = false;
    public Dialogue firstDialog;
    public TutorialType tutorialType = TutorialType.noTutorial;

    float lastTime;

    // Start is called before the first frame update
    void Start()
    {
        butterAmount = 0;
        rb = this.GetComponent<Rigidbody>();
        View_controller.OnViewChanged += switchOrientation;
        this.startPosition = transform.position;
        this.layer2D = LayerMask.NameToLayer("2D");
        this.layerGeneral = LayerMask.NameToLayer("Default");
        this.animator = GetComponent<Animator>();

        this.reset2DVisibility();

        FindObjectsOfType<DialogueManager>()[0].StartDialogue(this.firstDialog, this.tutorialType);
    }

    void OnDestroy() {
        View_controller.OnViewChanged -= switchOrientation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {     
        if(!this.blockPlayerInput) {
            if(Input.GetButtonDown("Jump") && isOnGround) {
                this.animator.SetBool("Jumping", true);
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
            this.animator.SetBool("Walking", horizontalInput != 0);

            if(horizontalInput > 0 != this.isPositiveMovement && horizontalInput != 0) {
                this.transform.Rotate(0f, this.isPositiveMovement? 180f: -180f, 0f);
                this.isPositiveMovement = !this.isPositiveMovement;
            }

            transform.Translate((this.isPositiveMovement? Vector3.forward : Vector3.back) * Time.deltaTime * speed * horizontalInput);


            if (Input.GetButtonDown("PauseGame")) {
                PauseMenu.P1.Pause();
            }
        }else {
            this.animator.SetBool("Walking", false);
        }

    }

    public void bounce() {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(Vector3.up * jumpHight/1.5f);
        isOnGround = true;
    }

    private async void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Ground")){
            this.animator.SetBool("Jumping", false);
            isOnGround = true;
        } else if(collision.gameObject.name == "Ground") {
            this.loseLifeReset();
            blockPlayerInput = true;
            await Task.Delay(2000);
            blockPlayerInput = false;
        }
    }
    private void switchOrientation() {
        if(!this.blockSwitch) {
            isSwitched = !isSwitched;
            this.transform.Rotate(0F, this.isSwitched? 90f : -90f , 0f);

            this.reset2DVisibility();
        }
    }

    public void loseLifeReset() {
        transform.position = this.startPosition;
        rb.velocity = new Vector3(0f,0f,0f);
        rb.angularVelocity = new Vector3(0f,0f,0f);
        ScoreManager.instance.loseLife();
        //GameOver Scene
        if (ScoreManager.instance.isGameOver()) {
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }

	this.reset2DVisibility();
    }

    private void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.CompareTag("Butter")) 
        {
            butterAmount++;
            collision.gameObject.SetActive(false);
            ScoreManager.instance.addPoint();
        }
    }
    
    private async void reset2DVisibility() {
        await Task.Delay(1);
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject currentObject in allObjects) {
            if (!currentObject.CompareTag("Player")) {
                currentObject.layer = this.layerGeneral;
            }
        }

        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(
            this.isSwitched ? this.planeThickness : this.planeDimensionsWidth,
            this.planeDimensionsHeight/2,
            this.isSwitched ? this.planeDimensionsWidth : this.planeThickness
            ));
        foreach (Collider collider in colliders) {
            if(collider.gameObject.CompareTag("Plane")) {
                continue;
            }
            collider.gameObject.layer = this.layer2D;
            foreach (Transform child in collider.gameObject.transform) {
                if(collider.gameObject.CompareTag("Plane")) {
                    continue;
                }
                child.gameObject.layer = this.layer2D;
            }
        }
    }
}
