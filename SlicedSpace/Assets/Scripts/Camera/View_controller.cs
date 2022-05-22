using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class View_controller : MonoBehaviour
{
    public delegate void ViewChanged();
    public static event ViewChanged OnViewChanged;

    private ParentConstraint pc;
    private bool isSwitched = false;

    // Start is called before the first frame update
    void Start()
    {
        pc = this.GetComponent<ParentConstraint>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("TurnPlane")) {
            OnViewChanged();
            isSwitched = !isSwitched;
            if(isSwitched) {
                pc.SetTranslationOffset(0, new Vector3(x: -10, y: 0, z: 0));
                pc.SetRotationOffset(0, new Vector3(x: 0, y: 90, z: 0));
            }else {
                pc.SetTranslationOffset(0, new Vector3(x: 0, y: 0, z: -10));
                pc.SetRotationOffset(0, new Vector3(x: 0, y: 0, z: 0));
            }
            
        }
    }
}
