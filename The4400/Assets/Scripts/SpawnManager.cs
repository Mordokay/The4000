using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

    public float elapsedTime;
    public float startTime;

    public float minimumTime;
    public float randomTime;

    // Use this for initialization
    void Start () {
        startTime = Time.time;
        minimumTime = 20.0f;
        randomTime = Random.Range(0, 5);

        Vector3 direction = Vector3.up;

        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);

        Vector3 forward = new Vector3(
               direction.x * cos - direction.y * sin,
               direction.x * sin + direction.y * cos,
               0f);

            int randomNum = Random.Range(0, 3);
            if (randomNum == 0)
            {
                Object newBallBall = Instantiate(Resources.Load("RedBall", typeof(GameObject)), this.GetComponent<Transform>().position, Quaternion.identity);
                GameObject newBallAux = (GameObject)newBallBall;
                newBallAux.GetComponent<Rigidbody2D>().velocity = forward * 10.0f;
            }
            else if (randomNum == 1)
            {
                Object newBallBall = Instantiate(Resources.Load("YellowBall", typeof(GameObject)), this.GetComponent<Transform>().position, Quaternion.identity);
                GameObject newBallAux = (GameObject)newBallBall;
                newBallAux.GetComponent<Rigidbody2D>().velocity = forward * 10.0f;
            }
            else if (randomNum == 2)
            {
                Object newBallBall = Instantiate(Resources.Load("BlueBall", typeof(GameObject)), this.GetComponent<Transform>().position, Quaternion.identity);
                GameObject newBallAux = (GameObject)newBallBall;
                newBallAux.GetComponent<Rigidbody2D>().velocity = forward * 10.0f;
            }

        }
	
	// Update is called once per frame
	void Update () {
        elapsedTime = Time.time - startTime;
        Vector3 direction = Vector3.up;

        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);

         Vector3 forward = new Vector3(
                direction.x * cos - direction.y * sin,
                direction.x * sin + direction.y * cos,
                0f);

        if (elapsedTime > (minimumTime + randomTime))
        {
            int randomNum = Random.Range(0, 3);
            if (randomNum == 0) {
                Object newBallBall = Instantiate(Resources.Load("RedBall", typeof(GameObject)), this.GetComponent<Transform>().position, Quaternion.identity);
                GameObject newBallAux = (GameObject)newBallBall;
                newBallAux.GetComponent<Rigidbody2D>().velocity = forward * 10.0f;
            }
            else if (randomNum == 1)
            {
                Object newBallBall = Instantiate(Resources.Load("YellowBall", typeof(GameObject)), this.GetComponent<Transform>().position, Quaternion.identity);
                GameObject newBallAux = (GameObject)newBallBall;
                newBallAux.GetComponent<Rigidbody2D>().velocity = forward * 10.0f;
            }
            else if(randomNum == 2) {
                Object newBallBall = Instantiate(Resources.Load("BlueBall", typeof(GameObject)), this.GetComponent<Transform>().position, Quaternion.identity);
                GameObject newBallAux = (GameObject)newBallBall;
                newBallAux.GetComponent<Rigidbody2D>().velocity = forward * 10.0f;
            }

            startTime = Time.time;
            randomTime = Random.Range(0, 15);
        }
	}
}
