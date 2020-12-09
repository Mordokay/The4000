using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class YellowBallManager : MonoBehaviour {

    float magnitude = 100.0f;
    GameObject gameManager;

    public int maxYellowballs = 3;

    float maximumSpeed = 4.0f;
    Rigidbody2D rb;

    public int yellowBallStage;

    private GameObject Player;
    private float Range;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        if(this.GetComponent<Rigidbody2D>().velocity == Vector2.zero) { 
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
        yellowBallStage += 1;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Chain")
        {
            if (yellowBallStage < 3)
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x * 0.8f, this.transform.localScale.y * 0.8f, 1);
                yellowBallStage += 1;

                Object newYellowBall = Instantiate(Resources.Load("YellowBall", typeof(GameObject)), this.GetComponent<Transform>().position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0), Quaternion.identity);
                GameObject newYellowBallAux = (GameObject)newYellowBall;

                newYellowBallAux.GetComponent<YellowBallManager>().yellowBallStage = yellowBallStage;
                if (yellowBallStage == 1)
                {
                    newYellowBallAux.transform.localScale = new Vector3(newYellowBallAux.transform.localScale.x * 0.8f, newYellowBallAux.transform.localScale.y * 0.8f, 1);
                }
                else if (yellowBallStage == 2)
                {
                    newYellowBallAux.transform.localScale = new Vector3(newYellowBallAux.transform.localScale.x * 0.8f * 0.8f, newYellowBallAux.transform.localScale.y * 0.8f * 0.8f, 1);
                }
                else if (yellowBallStage == 3)
                {
                    newYellowBallAux.transform.localScale = new Vector3(newYellowBallAux.transform.localScale.x * 0.8f * 0.8f * 0.8f, newYellowBallAux.transform.localScale.y * 0.8f * 0.8f * 0.8f, 1);
                }
            }
            else if (yellowBallStage == 3)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(0);
        }
    }

    void Update()
    {
        Vector2 velocity = new Vector2((transform.position.x - Player.transform.position.x) * 0.5f, (transform.position.y - Player.transform.position.y) * 0.5f);
        rb.velocity = -velocity;

        if (!gameManager.GetComponent<GameManager>().isPaused)
        {
            //float speed = Vector3.Magnitude(rb.velocity);  // test current object speed

            //if (speed > maximumSpeed)
            //{
            //float brakeSpeed = speed - maximumSpeed;  // calculate the speed decrease
            float brakeSpeed = Vector3.Magnitude(rb.velocity) - maximumSpeed;  // calculate the speed decrease
            Vector3 normalisedVelocity = rb.velocity.normalized;
                Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value

                rb.AddForce(-brakeVelocity);  // apply opposing brake force
            //}
        }
    }
}
