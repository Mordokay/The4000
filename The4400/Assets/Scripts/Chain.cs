using UnityEngine;
using System.Collections;

public class Chain : MonoBehaviour {

    ChainManager chainManager;

    // Use this for initialization
    void Start () {
        chainManager = GameObject.Find("GameManager").GetComponent<ChainManager>();
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Chain")
        {
            Physics2D.IgnoreCollision(coll, this.GetComponent<Collider2D>());
        }
        else if (coll.gameObject.tag == "redBall")
        {
            chainManager.breakChain = true;
        }
        else if (coll.gameObject.tag == "Borders")
        {
            chainManager.breakChain = true;
        }
        else if (coll.gameObject.tag == "blueBall")
        {
            chainManager.breakChain = true;
        }
        else if (coll.gameObject.tag == "yellowBall")
        {
            chainManager.breakChain = true;
        }
    }
}
