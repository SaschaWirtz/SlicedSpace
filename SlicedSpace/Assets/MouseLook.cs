using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Vector2 sensetivity;
    [SerializeField] private Vector2 accelration;
    [SerializeField] private float inputLagPeriod;
    [SerializeField] private float maxVerticalAngleFromHorizon;

    private Vector2 velocity;
    private Vector2 rotation;
    private Vector2 lastInputEvent;
    private float inputLagTimer;

    private float ClampVerticalAngle(float angle) {
        return Mathf.Clamp(angle, -this.maxVerticalAngleFromHorizon, this.maxVerticalAngleFromHorizon);
    }

    private Vector2 GetInput() {
        this.inputLagTimer += Time.deltaTime;

        Vector2 input = new Vector2(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y")
        );

        if((Mathf.Approximately(0, input.x) && Mathf.Approximately(0, input.y)) == false || this.inputLagTimer >= this.inputLagPeriod) {
            this.lastInputEvent = input;
            this.inputLagTimer = 0;
        }

        return this.lastInputEvent;
    }

    private void Update() {
        Vector2 wantedVelocity = this.GetInput() * this.sensetivity;

        this.velocity = new Vector2(
            Mathf.MoveTowards(this.velocity.x, wantedVelocity.x, this.accelration.x * Time.deltaTime),
            Mathf.MoveTowards(this.velocity.y, wantedVelocity.y, this.accelration.y * Time.deltaTime)
        );

        this.rotation += this.velocity * Time.deltaTime;
        this.rotation.y = this.ClampVerticalAngle(this.rotation.y);

        transform.localEulerAngles = new Vector3(this.rotation.y, this.rotation.x, 0);
    }
}
