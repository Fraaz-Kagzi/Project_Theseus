using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CubeAI : MonoBehaviour {

    public GameObject target;
    NavMeshAgent agent;
    public int close;
    public int lost;
    Vector3 randvec;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        //close enough to chase
        if (Vector3.Distance(target.transform.position, transform.position) < close) 
        {
            agent.SetDestination(target.transform.position);
            randvec = new Vector3(Random.Range(0.0f, 100.0f), 0, Random.Range(0.0f, 100.0f));
        }
        //random
        if (Vector3.Distance(target.transform.position, transform.position) > close && Vector3.Distance(target.transform.position, transform.position) < lost)
        {
            agent.SetDestination(randvec);
        }
        //new rand location
        if (Vector3.Distance(target.transform.position, randvec) < 10) 
        {
            randvec = new Vector3(Random.Range(0.0f, 100.0f), 0, Random.Range(0.0f, 100.0f));
            agent.SetDestination(randvec);
        }
        //prevent being lost
        if (Vector3.Distance(target.transform.position, transform.position) > lost)
        {
            agent.SetDestination(target.transform.position);
            randvec = new Vector3(Random.Range(0.0f, 100.0f), 0, Random.Range(0.0f, 100.0f));
        }
    }
}
