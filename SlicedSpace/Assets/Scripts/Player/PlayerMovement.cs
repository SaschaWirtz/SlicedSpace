using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float jumpHight = 400f;
    private Rigidbody rb;
    public float speed = 5.0f;
    private float horizontalInput;
    private bool isOnGround = true;
    private bool isSwitched = false;
    private int butterAmount;
    private Vector3 startPosition;
    private int layer2D;
    private int layerGeneral;
    public float planeThickness = 0.5f;
    public float planeDimensionsHeight = 10000f;
    public float planeDimensionsWidth = 10000f;

    // Start is called before the first frame update
    void Start()
    {
        butterAmount = 0;
        rb = this.GetComponent<Rigidbody>();
        View_controller.OnViewChanged += switchOrientation;
        this.startPosition = transform.position;
        this.layer2D = LayerMask.NameToLayer("2D");
        this.layerGeneral = LayerMask.NameToLayer("Default");

        this.reset2DVisibility();
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

        if (Input.GetButtonDown("PauseGame")) {
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Ground")){
            isOnGround = true;
        } else if(collision.gameObject.name == "Ground") {
            this.loseLifeReset();
        }
    }
    private void switchOrientation() {
        isSwitched = !isSwitched;

        this.reset2DVisibility();
    }

    private void loseLifeReset() {
        transform.position = this.startPosition;
        rb.velocity = new Vector3(0f,0f,0f);
        rb.angularVelocity = new Vector3(0f,0f,0f);
        ScoreManager.instance.loseLife();
        if (ScoreManager.instance.isGameOver()) {
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        }
    }

    private void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.CompareTag("Butter")) 
        {
            butterAmount++;
            collision.gameObject.SetActive(false);
            ScoreManager.instance.addPoint();
        }
    }

    private async Task reset2DVisibility() {
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
            collider.gameObject.layer = this.layer2D;
        }
    }
}
