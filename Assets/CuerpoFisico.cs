using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuerpoFisico : MonoBehaviour
{
    public float mass;
    
    public Vector3 velocity;
    Vector3 acceleration;

    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    public void PhysicsStep()
    {
        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime + 0.5f * acceleration * Time.deltaTime * Time.deltaTime; //x(t+1) = x(t) + v * t + 1/2 * a * t * t
        acceleration = Vector3.zero;
    }
}
