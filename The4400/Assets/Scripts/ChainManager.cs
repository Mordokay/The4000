using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChainManager : MonoBehaviour {

    public bool isChainning;
    public Vector3 chainStartPos;
    public Vector3 chainDirection;
    public GameObject chain;
    public GameObject chainArrow;
    public bool releasedArrow;
    public bool breakChain;
    public float speedChain;
    float timeSinceLastChain;
    public float chainDelay;

    public bool fingerReleased;

    public List<GameObject> Chains;

// Use this for initialization
void Start () {
        isChainning = false;
        releasedArrow = false;
        speedChain = 8.0f;
        timeSinceLastChain = 0.0f;
        chainDelay = 0.1f;
        fingerReleased = true;
    }
	
	// Update is called once per frame
	void Update () {

        timeSinceLastChain += Time.deltaTime;

        if (breakChain)
        {
            isChainning = false;
            releasedArrow = false;
            foreach (GameObject chain in Chains)
            {
                Destroy(chain);
            }
            Chains.Clear();
            breakChain = false;
        }
        if (isChainning)
        {
            timeSinceLastChain += Time.deltaTime;
            if (!releasedArrow)
            {
                Object myChainArrow = Instantiate(chainArrow, chainStartPos, Quaternion.identity);
                GameObject chainArrowAux = (GameObject)myChainArrow;
                Chains.Add(chainArrowAux);
                chainArrowAux.GetComponent<Rigidbody2D>().velocity = chainDirection.normalized * speedChain;

                float AngleRad = Mathf.Atan2(chainDirection.y, chainDirection.x);
                float angle = (180 / Mathf.PI) * AngleRad;
                chainArrowAux.GetComponent<Rigidbody2D>().rotation = angle - 90;

                timeSinceLastChain = 0.0f;
                releasedArrow = true;
            }
            else if((Chains[Chains.Count - 1].transform.position - chainStartPos).magnitude > 0.27f)
                //Chains[Chains.Count - 1].GetComponent<BoxCollider2D>().size.y * 0.1f)
            {
                Object myChain = Instantiate(chain, chainStartPos, Quaternion.identity);
                GameObject chainAux = (GameObject)myChain;
                Chains.Add(chainAux);
                chainAux.GetComponent<Rigidbody2D>().velocity = chainDirection.normalized * speedChain;

                float AngleRad = Mathf.Atan2(chainDirection.y, chainDirection.x);
                float angle = (180 / Mathf.PI) * AngleRad;
                chainAux.GetComponent<Rigidbody2D>().rotation = angle + 90;

                timeSinceLastChain = 0.0f;
            }
        }
    }
}
