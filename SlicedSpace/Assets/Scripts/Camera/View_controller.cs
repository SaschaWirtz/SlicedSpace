using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class View_controller : MonoBehaviour
{
    public delegate void ViewChanged();
    public static event ViewChanged OnViewChanged;
    [SerializeField] public bool blockSwitch = false;

    private PositionConstraint pc;
    private bool isSwitched = false;

    // Start is called before the first frame update
    void Start()
    {
        pc = this.GetComponent<PositionConstraint>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("TurnPlane") && !this.blockSwitch) {
            OnViewChanged();
            isSwitched = !isSwitched;
            if(isSwitched) {
                pc.translationOffset = new Vector3(x: -10, y: 0, z: 0);
                this.transform.Rotate(0f, 90f, 0f);
            }else {
                pc.translationOffset =  new Vector3(x: 0, y: 0, z: -10);
                this.transform.Rotate(0f, -90f, 0f);
            }
            
        }
    }
}
