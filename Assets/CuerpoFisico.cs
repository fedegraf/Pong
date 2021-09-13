using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuerpoFisico : MonoBehaviour
{
    public float masa;
    
    public Vector3 velocidad;
    Vector3 aceleracion;

    public void ApplyForce(Vector3 fuerza)
    {
        aceleracion += fuerza / masa;
    }

    public void PasoDeFisica()
    {
        velocidad += aceleracion * Time.deltaTime;
        transform.position += velocidad * Time.deltaTime + 0.5f * aceleracion * Time.deltaTime * Time.deltaTime; //x(t+1) = x(t) + v * t + 1/2 * a * t * t
        aceleracion = Vector3.zero;
    }
}
