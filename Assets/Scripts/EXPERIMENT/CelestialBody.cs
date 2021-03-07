using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    public Vector3 initialVelocity = new Vector3(0f, 0f, 0f);
    public Rigidbody rb;

    //CelestialBody[] celestialBodies;

    private bool firstIteration = true;

    private void FixedUpdate()
    {
        rb.AddTorque(initialVelocity);
        /*if (firstIteration)
        {
            rb.AddForce(initialVelocity, ForceMode.Acceleration);
            firstIteration = false;
        }

        celestialBodies = FindObjectsOfType<CelestialBody>();

        foreach (CelestialBody body in celestialBodies)
        {
            if (body != this)
                GravitationalPull(body);
        }*/
    }

    public void GravitationalPull(CelestialBody otherBody)
    {
        Rigidbody rbToAttract = otherBody.rb;

        Vector3 direction = this.rb.position - rbToAttract.position;
        float distance = direction.magnitude;
        float gravitationalConstant = 6.675f;// 6.674 * Mathf.Pow(10, -11);

        float forceMagnitude = gravitationalConstant * ((rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2));

        Vector3 force = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(force);
    }
}
