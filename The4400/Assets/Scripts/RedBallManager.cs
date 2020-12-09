using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RedBallManager : MonoBehaviour {

    float magnitude = 100.0f;
    GameObject gameManager;

    float maximumSpeed = 6.0f;
    Rigidbody2D rb;

    public int redBallStage;

    void Start () {
        gameManager = GameObject.Find("GameManager");
        rb = GetComponent<Rigidbody2D>();
        if (this.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            int signal1 = 1;
            int signal2 = 1;
            if (Random.value > 0.5) signal1 = -1;
            if (Random.value > 0.5) signal2 = -1;

            this.GetComponent<Rigidbody2D>().velocity =
                new Vector3(Random.value * magnitude * signal1, Random.value * magnitude * signal2, 0.0f);

            this.GetComponent<Rigidbody2D>().velocity = Vector3.ClampMagnitude(this.GetComponent<Rigidbody2D>().velocity, maximumSpeed);
        }
    }

    public void incrementStage()
    {
        redBallStage += 1;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Chain")
        {
            if (redBallStage < 3)
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x * 0.8f, this.transform.localScale.y * 0.8f, 1);
                redBallStage += 1;

                Object newRedBall = Instantiate(Resources.Load("Redball", typeof(GameObject)), this.GetComponent<Transform>().position, Quaternion.identity);
                GameObject newRedBallAux = (GameObject)newRedBall;

                newRedBallAux.GetComponent<RedBallManager>().redBallStage = redBallStage;
                if (redBallStage == 1)
                {
                    newRedBallAux.transform.localScale = new Vector3(newRedBallAux.transform.localScale.x * 0.8f, newRedBallAux.transform.localScale.y * 0.8f, 1);
                }
                else if (redBallStage == 2)
                {
                    newRedBallAux.transform.localScale = new Vector3(newRedBallAux.transform.localScale.x * 0.8f * 0.8f, newRedBallAux.transform.localScale.y * 0.8f * 0.8f, 1);
                }
                else if (redBallStage == 3)
                {
                    newRedBallAux.transform.localScale = new Vector3(newRedBallAux.transform.localScale.x * 0.8f * 0.8f * 0.8f, newRedBallAux.transform.localScale.y * 0.8f * 0.8f * 0.8f, 1);
                }
            }
            else if (redBallStage == 3) {
                Destroy(this.gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player") {
            SceneManager.LoadScene(0);
        }
    }

    void Update()
    {
        if (!gameManager.GetComponent<GameManager>().isPaused) {
            //float speed = Vector3.Magnitude(rb.velocity);  // test current object speed
            Debug.Log(rb.velocity.normalized);

            //if (speed > maximumSpeed)
            // {
            //float brakeSpeed = speed - maximumSpeed;  // calculate the speed decrease
            float brakeSpeed = Vector3.Magnitude(rb.velocity) - maximumSpeed;  // calculate the speed decrease
            Vector3 normalisedVelocity = rb.velocity.normalized;
                Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value

                rb.AddForce(-brakeVelocity);  // apply opposing brake force
            //}
        }
    }
}
