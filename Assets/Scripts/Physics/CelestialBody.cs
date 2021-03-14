using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    public Vector3 initialVelocity = new Vector3(0f, 0f, 0f);

    public Rigidbody rigidbody;
    public float mass;

    CelestialBody[] celestialBodies;

    public CelestialBody bodyToOrbit;

    //private bool firstIteration = true;

    private void Start()
    {
        celestialBodies = FindObjectsOfType<CelestialBody>();

        mass = CalculateMass();

        rigidbody.mass = this.mass;

        //initialVelocity = CalculateInitialVelocityForOrbit();

        rigidbody.AddForce(initialVelocity, ForceMode.Acceleration);
    }

    private void FixedUpdate()
    {
        foreach (CelestialBody body in celestialBodies)
        {
            if (body != this)
                AttractBody(body);
        }
    }

    private float CalculateMass()
    {
        return mass;
    }

    private Vector3 CalculateInitialVelocityForOrbit()
    {
        if (bodyToOrbit == null)
            return new Vector3(0f, 0f, 0f);
        else
        {
            Vector3 direction = rigidbody.position - bodyToOrbit.rigidbody.position;
            float radius = direction.magnitude;

            float velocityMagnitude = Mathf.Sqrt((Universe.gravitationalConstant * bodyToOrbit.mass) / radius);

            //Vector3 velocity = direction.normalized * velocityMagnitude;

            return new Vector3(velocityMagnitude, 0f, 0f);
        }
    }

    public void AttractBody(CelestialBody otherBody)
    {
        Rigidbody objectToAttract = otherBody.rigidbody;

        Vector3 direction = rigidbody.position - objectToAttract.position;
        float distance = direction.magnitude;

        float forceMagnitude = Universe.gravitationalConstant * ((rigidbody.mass * objectToAttract.mass) / Mathf.Pow(distance, 2));

        Vector3 force = direction.normalized * forceMagnitude;

        objectToAttract.AddForce(force);
    }
}
