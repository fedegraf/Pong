using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletaScript : CuerpoFisico
{
    public float velocidadMaxima;
    public float fuerzaMaxima;
    public float velocidadDeseada;
    public float aceleracionTeclas;
    public float fuerzaTeclas;
    Vector3 aceleracionActual;
    float paredHorizontal = 5f;
    [SerializeField] float speed = 4f;
    public float altura = 1.5f;
    [SerializeField] KeyCode keyUp;
    [SerializeField] KeyCode keyDown;
    void Start()
    {
 
    }

    // Update is called once per frame
    public void Update()
    {
        velocidadDeseada = 0f;
        //Vector3 fuerza = Vector3.zero;

        if (Input.GetKey(keyUp))                  //Depende de la tecla que toquemos, la velocidad máxima será hacia arriba o hacia abajo
        {
            velocidadDeseada += velocidadMaxima;   
        }
        if (Input.GetKey(keyDown))
        {
            velocidadDeseada -= velocidadMaxima;
        }

        float fuerza = masa * (velocidadDeseada - velocidad.y) / Time.deltaTime;       // Calculamos la fuerza que vamos a hacer acercandonos a la velocidad deseada
        fuerza = Mathf.Clamp(fuerza, -fuerzaMaxima, fuerzaMaxima);                     // La limitamos determinando una fuerza máxima
        ApplyForce(Vector3.up * fuerza);                                               // La aplicamos

        //transform.position += velocidad * Time.deltaTime + 0.5f * aceleracionActual * Time.deltaTime * Time.deltaTime; //x(t+1) = x(t) + v * t + 1/2 * a * t * t

        if (transform.position.y > paredHorizontal - altura)                                    
        {
            transform.position = new Vector3(transform.position.x, paredHorizontal - altura);
            velocidad.y = 0f;
        }
        if(transform.position.y < -paredHorizontal + altura)                                       // Frenamos al chocar contra el techo y contra el suelo
        {
            transform.position = new Vector3(transform.position.x, -paredHorizontal + altura);
            velocidad.y = 0f;
        }

        PasoDeFisica();
    }
}
