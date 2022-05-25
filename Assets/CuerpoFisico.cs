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
        // position (time +1) = position(time) + velocity * time + 1/2 * aceleration * time * time
        transform.position += velocity * Time.deltaTime + 0.5f * acceleration * Time.deltaTime * Time.deltaTime;
        acceleration = Vector3.zero;
    }
}
