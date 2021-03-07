using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 initialForce = new Vector3(0, 0, 0);

    private bool firstIteration = true;

    Attractor[] attractors;

    private void Start()
    {
        //rb.AddForce(Random.insideUnitSphere * initialForce, ForceMode.Acceleration);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (firstIteration)
        {
            rb.AddForce(initialForce, ForceMode.Acceleration);
            firstIteration = false;
        }

        attractors = FindObjectsOfType<Attractor>();

        foreach (Attractor attractor in attractors)
        {
            if (attractor != this)
                Attract(attractor);
        }
    }

    void Attract(Attractor objectToAttract)
    {
        Rigidbody rbToAttract = objectToAttract.rb;

        Vector3 direction = this.rb.position - rbToAttract.position;
        float distance = direction.magnitude;
        float gravitationalConstant = 6.675f;// 6.674 * Mathf.Pow(10, -11);

        float forceMagnitude = gravitationalConstant * ((rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2));

        Vector3 force = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(force);


        /*
        Rigidbody rbToAttract = objectToAttract.rb;

        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;

        float forceMagnitude = (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(force);
        */
    }
}
