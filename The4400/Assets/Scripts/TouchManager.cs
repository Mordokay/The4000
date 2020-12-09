using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour {

    Touch myTouch1;
    Touch myTouch2;
    public PlayerMovement playerMovement;

    public GameObject leftTouchPad;
    public GameObject rightTouchPad;
    public GameObject springLeft;
    public GameObject springRight;

    public Vector3 startLeftTouch;
    public Vector3 startRightTouch;

    ChainManager chainManager;

    Vector3 touchAuxPos;

    void Start () {
        leftTouchPad.SetActive(false);
        rightTouchPad.SetActive(false);
        chainManager = this.GetComponent<ChainManager>();
    }

    void Update() {

        if (Input.touchCount > 0)
        {
            myTouch1 = Input.GetTouch(0);
            if (myTouch1.phase == TouchPhase.Began)
            {
                touchAuxPos = Camera.main.ScreenToWorldPoint(new Vector3(myTouch1.position.x, myTouch1.position.y, -Camera.main.transform.position.z));
                touchAuxPos.z = -0.001f;
                if (isTouchingLeft(myTouch1.position.x / Screen.width))
                {
                    leftTouchPad.SetActive(true);
                    leftTouchPad.transform.position = touchAuxPos;
                    startLeftTouch = touchAuxPos;
                }
                else
                {
                    rightTouchPad.SetActive(true);
                    rightTouchPad.transform.position = touchAuxPos;
                    startRightTouch = touchAuxPos;
                }
            }
            else if (myTouch1.phase == TouchPhase.Moved)
            {
                touchAuxPos = Camera.main.ScreenToWorldPoint(new Vector3(myTouch1.position.x, myTouch1.position.y, -Camera.main.transform.position.z));
                touchAuxPos.z = -0.001f;

                if (isTouchingLeft(myTouch1.position.x / Screen.width))
                {
                    springLeft.SetActive(true);
                    springLeft.transform.position = (touchAuxPos + leftTouchPad.transform.position) / 2;
                    springLeft.transform.localScale = new Vector3(0.4f , (touchAuxPos - leftTouchPad.transform.position).magnitude / 2, 1);

                    float AngleRad = Mathf.Atan2((touchAuxPos - leftTouchPad.transform.position).y, (touchAuxPos - leftTouchPad.transform.position).x);
                    float angle = (180 / Mathf.PI) * AngleRad;
                    springLeft.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                    playerMovement.movementTouchpad = touchAuxPos - leftTouchPad.transform.position;
                }
                else
                {
                    if (chainManager.fingerReleased == true)
                    {
                        chainManager.breakChain = true;
                        chainManager.fingerReleased = false;
                    }

                    springRight.SetActive(true);
                    springRight.transform.position = (touchAuxPos + rightTouchPad.transform.position) / 2;
                    springRight.transform.localScale = new Vector3(0.4f, (touchAuxPos - rightTouchPad.transform.position).magnitude / 2, 1);

                    float AngleRad = Mathf.Atan2((touchAuxPos - rightTouchPad.transform.position).y, (touchAuxPos - rightTouchPad.transform.position).x);
                    float angle = (180 / Mathf.PI) * AngleRad;
                    springRight.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                    
                    playerMovement.bulletTouchpad = touchAuxPos - rightTouchPad.transform.position;
                }
            }
            else if(myTouch1.phase == TouchPhase.Ended)
            {
                if (isTouchingLeft(myTouch1.position.x / Screen.width))
                {
                    leftTouchPad.SetActive(false);
                    springLeft.SetActive(false);
                    playerMovement.movementTouchpad = Vector3.zero;
                }
                else
                {
                    chainManager.fingerReleased = true;
                    rightTouchPad.SetActive(false);
                    springRight.SetActive(false);
                    playerMovement.bulletTouchpad = Vector3.zero;
                }
            }
        }

        if (Input.touchCount > 1)
        {
            myTouch2 = Input.GetTouch(1);
            
            if (myTouch2.phase == TouchPhase.Began)
            {
                
                touchAuxPos = Camera.main.ScreenToWorldPoint(new Vector3(myTouch2.position.x, myTouch2.position.y, -Camera.main.transform.position.z));
                touchAuxPos.z = -0.001f;
                if (isTouchingLeft(myTouch2.position.x / Screen.width))
                {
                    leftTouchPad.SetActive(true);
                    leftTouchPad.transform.position = touchAuxPos;
                    startLeftTouch = touchAuxPos;
                }
                else
                {
                    rightTouchPad.SetActive(true);
                    rightTouchPad.transform.position = touchAuxPos;
                    startRightTouch = touchAuxPos;
                }
            }
            else if (myTouch2.phase == TouchPhase.Moved)
            {
                touchAuxPos = Camera.main.ScreenToWorldPoint(new Vector3(myTouch2.position.x, myTouch2.position.y, -Camera.main.transform.position.z));
                touchAuxPos.z = -0.001f;
                if (isTouchingLeft(myTouch2.position.x / Screen.width))
                {
                    springLeft.SetActive(true);
                    springLeft.transform.position = (touchAuxPos + leftTouchPad.transform.position) / 2;
                    springLeft.transform.localScale = new Vector3(0.4f, (touchAuxPos - leftTouchPad.transform.position).magnitude / 2, 1);

                    float AngleRad = Mathf.Atan2((touchAuxPos - leftTouchPad.transform.position).y, (touchAuxPos - leftTouchPad.transform.position).x);
                    float angle = (180 / Mathf.PI) * AngleRad;
                    springLeft.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

                    playerMovement.movementTouchpad = touchAuxPos - leftTouchPad.transform.position;
                }
                else
                {
                    if (chainManager.fingerReleased == true)
                    {
                        chainManager.breakChain = true;
                        chainManager.fingerReleased = false;
                    }

                    springRight.SetActive(true);
                    springRight.transform.position = (touchAuxPos + rightTouchPad.transform.position) / 2;
                    springRight.transform.localScale = new Vector3(0.4f, (touchAuxPos - rightTouchPad.transform.position).magnitude / 2, 1);

                    float AngleRad = Mathf.Atan2((touchAuxPos - rightTouchPad.transform.position).y, (touchAuxPos - rightTouchPad.transform.position).x);
                    float angle = (180 / Mathf.PI) * AngleRad;
                    springRight.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                    playerMovement.bulletTouchpad = touchAuxPos - rightTouchPad.transform.position;
                }
            }
            else if (myTouch2.phase == TouchPhase.Ended)
            {
                if (isTouchingLeft(myTouch2.position.x / Screen.width))
                {
                    leftTouchPad.SetActive(false);
                    springLeft.SetActive(false);
                    playerMovement.movementTouchpad = Vector3.zero;
                }
                else
                {
                    chainManager.fingerReleased = true;
                    rightTouchPad.SetActive(false);
                    springRight.SetActive(false);
                    playerMovement.bulletTouchpad = Vector3.zero;
                }
            }
        }
    }

    bool isTouchingLeft(float x) {
        return x <=0.5;
    }
}
