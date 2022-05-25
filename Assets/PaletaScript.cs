using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletaScript : CuerpoFisico
{
    public float maxVelocity;
    public float maxForce;
    public float wantedVelocity;
    public float inputAcceleration;
    public float inputForce;
    Vector3 currentAcceleration;
    float horizontalWall = 5f;
    [SerializeField] float speed = 4f;
    public float height = 1.5f;
    [SerializeField] KeyCode keyUp;
    [SerializeField] KeyCode keyDown;

    // Update is called once per frame
    public void Update()
    {
        wantedVelocity = 0f;

        // It depends on the key we play, the maximum velocity will be up or down
        if (Input.GetKey(keyUp))
        {
            wantedVelocity += maxVelocity;   
        }
        if (Input.GetKey(keyDown))
        {
            wantedVelocity -= maxVelocity;
        }

        // We calculate the force that we are going to make approaching the desired velocity
        float force = mass * (wantedVelocity - velocity.y) / Time.deltaTime;
        // We limit it by determining a maximum force
        force = Mathf.Clamp(force, -maxForce, maxForce);
        ApplyForce(Vector3.up * force);

        if (transform.position.y > horizontalWall - height)                                    
        {
            transform.position = new Vector3(transform.position.x, horizontalWall - height);
            velocity.y = 0f;
        }
        // We stop when hitting the ceiling and the ground
        if(transform.position.y < -horizontalWall + height)
        {
            transform.position = new Vector3(transform.position.x, -horizontalWall + height);
            velocity.y = 0f;
        }

        PhysicsStep();
    }
}
