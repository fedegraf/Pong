using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Text playerOneScore;
    public Text playerTwoScore;
    public Canvas pauseCanvas;


    static public bool paused;

    private void Awake()
    {
        paused = true;
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
