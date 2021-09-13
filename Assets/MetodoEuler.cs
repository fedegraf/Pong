using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetodoEuler : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    Vector3 velocidad;
    Vector3 aceleracion;
    float radioPowerUp = 0.5f;
    public PaletaScript paletaDerecha;
    public PaletaScript paletaIzquierda;
    public Transform powerUp;
    float paredHorizontal = 5f;
    float paredVertical = 8.9f;
    float radio = 0.25f;
    bool unpaused;
    [SerializeField] CanvasManager canvas;

    bool gravityOn;

    Vector3 gravedad = new Vector3(0, -9.8f, 0);

    int puntajeIzq = 0;
    int puntajeDer = 0;
    void Start()
    {
        if (!CanvasManager.paused) velocidad = new Vector3(speed, speed, 0f);
        else if (CanvasManager.paused) velocidad = Vector3.zero;
        gravityOn = false;
        unpaused = false;
    }

    void Update()
    {
        if (gravityOn && !CanvasManager.paused) velocidad += gravedad * Time.deltaTime;
        else if (!gravityOn && !CanvasManager.paused && !unpaused)    // Si no hay gravedad y el juego está en pausa, se le da la velocidad a la bola
        {
            unpaused = true;
            velocidad = new Vector3(speed, speed, 0f);
        }


        if (CanvasManager.paused)
        {
            velocidad = Vector3.zero;
            unpaused = false;
        }


        transform.position += velocidad * Time.deltaTime;       // Se hace el movimiento de la bola usando su velocidad

        if(transform.position.y >= paredHorizontal - radio)     // Si tocamos el techo, hacemos que la velocidad sea hacia abajo en el eje Y
        {
            velocidad.y = -Mathf.Abs(velocidad.y);
        }
        if(transform.position.y <= -paredHorizontal + radio)
        {
            velocidad.y = Mathf.Abs(velocidad.y);               // Si tocamos el piso, hacemos que la velocidad sea hacia arriba en el eje Y
        }
        if(transform.position.x >= paredVertical - radio)       // Si tocamos la pared de la derecha...
        {
            float paletaY = paletaDerecha.transform.position.y;
            if (transform.position.y <= paletaY + paletaDerecha.altura && transform.position.y >= paletaY - paletaDerecha.altura)  // nos fijamos si la pelota está tocando a la paleta usando sus alturas
            {
                velocidad.x = -Mathf.Abs(velocidad.x);    //si está tocando, convertimos la velocidad x en negativa haciendo que vaya a la izquierda, si no lo hace, punto para la izquierda.
            }
            else                                          
            {
                transform.position = Vector3.zero;
                puntajeIzq++;
                canvas.puntajeIzq.text = puntajeIzq.ToString();
                CanvasManager.paused = true;
                Debug.Log($"Puntaje: Izquierda {puntajeIzq} - {puntajeDer} Derecha");
            }
        }
        if (transform.position.x <= -paredVertical + radio)           // Lo mismo para la pared izquierda ahora
        {
            float paletaY = paletaIzquierda.transform.position.y;
            if (transform.position.y <= paletaY + paletaDerecha.altura && transform.position.y >= paletaY - paletaDerecha.altura)
            {
                velocidad.x = Mathf.Abs(velocidad.x);
            }
            else
            {
                transform.position = Vector3.zero;
                puntajeDer++;
                canvas.puntajeDer.text = puntajeDer.ToString();
                CanvasManager.paused = true;
                Debug.Log($"Puntaje: Izquierda {puntajeIzq} - {puntajeDer} Derecha");
            }
        }

        if(powerUp != null)
        {
            float sumaDeRadios = radio + radioPowerUp;
            if(Vector2.SqrMagnitude(transform.position - powerUp.position) < sumaDeRadios * sumaDeRadios)
            {
                Destroy(powerUp.gameObject);
                //accion del powerup
            }
        }

        /*if (Input.GetKey(KeyCode.D))
        {
            if(Input.GetKey(KeyCode.LeftShift)) transform.position += Vector3.right * (speed + 2f) * Time.deltaTime;
            else transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftShift)) transform.position -= Vector3.right * (speed + 2f) * Time.deltaTime;
            else transform.position -= Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift)) transform.position += Vector3.up* (speed + 2f) * Time.deltaTime;
            else transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.LeftShift)) transform.position -= Vector3.up * (speed + 2f) * Time.deltaTime;
            else transform.position -= Vector3.up * speed * Time.deltaTime;
        }*/
    }
}
