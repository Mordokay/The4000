using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BlueBallManager : MonoBehaviour {

    float magnitude = 100.0f;
    public float timeToSplit = 6.0f;
    GameObject gameManager;

    float startTime;
    float elapsedTime = 0.0f;

    float maximumSpeed = 4.0f;
    Rigidbody2D rb;
    public int numSplits = 2;
    public int blueBallStage;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        rb = GetComponent<Rigidbody2D>();
        startTime = Time.time;
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

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Chain")
        {
            if (blueBallStage < 3)
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x * 0.8f, this.transform.localScale.y * 0.8f, 1);
                blueBallStage += 1;
                startTime = Time.time;

                Object newBlueBall = Instantiate(Resources.Load("Blueball", typeof(GameObject)), this.GetComponent<Transform>().position, Quaternion.identity);
                GameObject newBlueBallAux = (GameObject)newBlueBall;
                newBlueBallAux.GetComponent<BlueBallManager>().blueBallStage = blueBallStage;

                if (blueBallStage == 1)
                {
                    newBlueBallAux.transform.localScale = new Vector3(newBlueBallAux.transform.localScale.x * 0.8f, newBlueBallAux.transform.localScale.y * 0.8f, 1);
                    numSplits = 1;
                    newBlueBallAux.GetComponent<BlueBallManager>().numSplits = numSplits;
                }
                else if (blueBallStage == 2)
                {
                    newBlueBallAux.transform.localScale = new Vector3(newBlueBallAux.transform.localScale.x * 0.8f * 0.8f, newBlueBallAux.transform.localScale.y * 0.8f * 0.8f, 1);
                    numSplits = 0;
                    newBlueBallAux.GetComponent<BlueBallManager>().numSplits = numSplits;
                }
            }
            else if (blueBallStage == 3)
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
        if (!gameManager.GetComponent<GameManager>().isPaused)
        {
            elapsedTime = Time.time - startTime;
            if (elapsedTime > timeToSplit)
            {
                if (numSplits > 0)
                {
                    numSplits -= 1;
                    Object blueBall = Instantiate(Resources.Load("Blueball", typeof(GameObject)), this.GetComponent<Transform>().position, Quaternion.identity);
                    GameObject blueBallAux = (GameObject)blueBall;
                    blueBallAux.GetComponent<BlueBallManager>().numSplits = numSplits;
                    blueBallAux.GetComponent<BlueBallManager>().blueBallStage = blueBallStage;
                    if (blueBallStage == 1)
                    {
                        blueBallAux.transform.localScale = new Vector3(blueBallAux.transform.localScale.x * 0.8f, blueBallAux.transform.localScale.y * 0.8f, 1);
                    }
                    else if (blueBallStage == 2)
                    {
                        blueBallAux.transform.localScale = new Vector3(blueBallAux.transform.localScale.x * 0.8f * 0.8f, blueBallAux.transform.localScale.y * 0.8f * 0.8f, 1);
                    }
                    startTime = Time.time;
                }
            }

            //float speed = Vector3.Magnitude(rb.velocity);

            //if (speed > maximumSpeed)
            // {
            //    float brakeSpeed = speed - maximumSpeed;  // calculate the speed decrease
            float brakeSpeed = Vector3.Magnitude(rb.velocity) - maximumSpeed;  // calculate the speed decrease
            Vector3 normalisedVelocity = rb.velocity.normalized;
                Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value

                rb.AddForce(-brakeVelocity);  // apply opposing brake force
            //}
        }
    }
}
