using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;



public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    public float speed = 5.0f;
    private Animator animator;
    private bool isPositiveMovement = true;
    private float horizontalInput = 1f;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {             
        if(this.horizontalInput > 0 != this.isPositiveMovement && this.horizontalInput != 0) {
            this.transform.Rotate(0f, this.isPositiveMovement? 180f: -180f, 0f);
            this.isPositiveMovement = !this.isPositiveMovement;
        }

        transform.Translate((this.isPositiveMovement? Vector3.forward : Vector3.back) * Time.deltaTime * speed * horizontalInput);
    }

    void FixedUpdate() {
        int collisions = 0;

        Vector3 position = transform.Find("Detector").transform.position;
        Collider[] colliders = Physics.OverlapBox(position, new Vector3(
            0.1f,
            0.85f,
            0.6f
        ));
        foreach(Collider collider in colliders) {
            if(!(collider.gameObject.CompareTag("Player") || collider.gameObject.CompareTag("Butter") || collider.gameObject.CompareTag("Plane"))) {
                Debug.Log(collider.gameObject.name);
                collisions++;
            }
        }
        if(collisions == 0) {
            this.horizontalInput *= -1;
        }

        if(transform.Find("HitBox").GetComponent<HitBoxDetection>() != null) {
            if(transform.Find("HitBox").GetComponent<HitBoxDetection>().IsHit() && !transform.Find("HurtBox").GetComponent<HitBoxDetection>().IsHit()) {
                animator.SetBool("Dieing", true);
                transform.Find("HitBox").GetComponent<HitBoxDetection>().destroy();
                transform.Find("HurtBox").GetComponent<HitBoxDetection>().destroy();
                this.speed = 0;
                GameObject.Find("Player").GetComponent<PlayerMovement>().bounce();
                Destroy(gameObject, 2);
            }else if(transform.Find("HurtBox").GetComponent<HitBoxDetection>().IsHit()) {
                GameObject.Find("Player").GetComponent<PlayerMovement>().loseLifeReset();
                transform.Find("HitBox").GetComponent<HitBoxDetection>().reset();
                transform.Find("HurtBox").GetComponent<HitBoxDetection>().reset();
            }
        }
    }
}
