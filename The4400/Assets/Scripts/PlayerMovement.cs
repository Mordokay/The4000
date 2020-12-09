using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float maximumSpeed;
    public float speedMovement;
    GameObject gameManager;
    public ChainManager chainManager;

    public Vector3 movementTouchpad;
    public Vector3 bulletTouchpad;
    public Vector3 leftTouch;
    public Vector3 rightTouch;


    Vector3 normalizedmovement;

    Rigidbody2D rb;
    Camera cam;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager");
        cam = Camera.main;
        maximumSpeed = 6.0f;
        speedMovement = 5.0f;
    }

    // Update is called once per frame
    void Update () {
        //Camera follow player
        cam.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, cam.transform.position.z) ;
        //print("Player pos: " + this.transform.position + "Camera Pos: " + cam.transform.position);
        float AngleRad;
        float angle;
        if (!gameManager.GetComponent<GameManager>().isPaused) {
            if(movementTouchpad != Vector3.zero) { 
                AngleRad = Mathf.Atan2(movementTouchpad.y , movementTouchpad.x);
                angle = (180 / Mathf.PI) * AngleRad;
                rb.rotation = angle - 90;
            }
            if (bulletTouchpad != Vector3.zero) {
                AngleRad = Mathf.Atan2(bulletTouchpad.y, bulletTouchpad.x);
                angle = (180 / Mathf.PI) * AngleRad;
                rb.rotation = angle - 90;

                if ( bulletTouchpad.magnitude > 1) {

                    if (!chainManager.isChainning)
                    {
                        chainManager.isChainning = true;
                        chainManager.chainStartPos = this.transform.position + (bulletTouchpad.normalized * 0.5f);
                        chainManager.chainDirection = bulletTouchpad;
                    }
                }
            }

            if (movementTouchpad != Vector3.zero)
            {
                normalizedmovement = movementTouchpad.normalized;
                this.GetComponent<Rigidbody2D>().velocity =
                    new Vector2(normalizedmovement.x, normalizedmovement.y) * speedMovement;
            }

            float speed = Vector3.Magnitude(rb.velocity);  // test current object speed
            if (speed > maximumSpeed)
            {
                float brakeSpeed = speed - maximumSpeed;  // calculate the speed decrease

                Vector3 normalisedVelocity = rb.velocity.normalized;
                Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value

                rb.AddForce(-brakeVelocity);  // apply opposing brake force
            }
        }
    }
}
