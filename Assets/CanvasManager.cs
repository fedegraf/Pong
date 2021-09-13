using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Text puntajeIzq;
    public Text puntajeDer;
    public Canvas pauseCanvas;


    static public bool paused;

    private void Awake()
    {
        paused = true;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            paused = false;
        }
        
        if (!paused)
        {
            Time.timeScale = 1;
            pauseCanvas.gameObject.SetActive(false);
        }

        else if (paused)
        { 
            pauseCanvas.gameObject.SetActive(true);
        }
    }
}
