using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetodoEuler : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    Vector3 velocity;
    Vector3 acceleration;
    float radioPowerUp = 0.5f;
    public PaletaScript playerOne;
    public PaletaScript playerTwo;
    public Transform powerUp;
    float horizontalWall = 5f;
    float verticalWall = 8.9f;
    float radio = 0.25f;
    bool unpaused;
    [SerializeField] CanvasManager canvas;
    bool gravityOn;
    Vector3 gravity = new Vector3(0, -9.8f, 0);
    int playerOneScore = 0;
    int playerTwoScore = 0;
    
    
    void Start()
    {
        if (!CanvasManager.paused) velocity = new Vector3(speed, speed, 0f);
        else if (CanvasManager.paused) velocity = Vector3.zero;
        gravityOn = false;
        unpaused = false;
    }

    void Update()
    {
        if (gravityOn && !CanvasManager.paused) velocity += gravity * Time.deltaTime;
        // If there is no gravity and the game is unpaused, velocity is given to the ball
        else if (!gravityOn && !CanvasManager.paused && !unpaused)
        {
            unpaused = true;
            velocity = new Vector3(speed, speed, 0f);
        }


        if (CanvasManager.paused)
        {
            velocity = Vector3.zero;
            unpaused = false;
        }
        
        // The movement of the ball is made using its velocity
        transform.position += velocity * Time.deltaTime;

        // If we touch the ceiling, we make the velocity down the Y axis
        if(transform.position.y >= horizontalWall - radio)
        {
            velocity.y = -Mathf.Abs(velocity.y);
        }
        if(transform.position.y <= -horizontalWall + radio)
        {
            // If we touch the ground, we make the velocity be up on the Y axis
            velocity.y = Mathf.Abs(velocity.y);
        }
        if(transform.position.x >= verticalWall - radio)
        {
            float paletaY = playerOne.transform.position.y;
            // we check if the ball is touching the paddle using its heights
            if (transform.position.y <= paletaY + playerOne.height && transform.position.y >= paletaY - playerOne.height)
            {
                // if it's touching, we convert velocity x to negative making it go to the left, if it doesn't, point to the player
                velocity.x = -Mathf.Abs(velocity.x);
            }
            // if it's not touching, it's a goal
            else                                          
            {
                transform.position = Vector3.zero;
                playerOneScore++;
                canvas.playerOneScore.text = playerOneScore.ToString();
                CanvasManager.paused = true;
                Debug.Log($"Score: Left {playerOneScore} - {playerTwoScore} Right");
            }
        }
        // same for the left wall
        if (transform.position.x <= -verticalWall + radio)
        {
            float paletaY = playerTwo.transform.position.y;
            if (transform.position.y <= paletaY + playerOne.height && transform.position.y >= paletaY - playerOne.height)
            {
                velocity.x = Mathf.Abs(velocity.x);
            }
            else
            {
                transform.position = Vector3.zero;
                playerTwoScore++;
                canvas.playerTwoScore.text = playerTwoScore.ToString();
                CanvasManager.paused = true;
                Debug.Log($"Score: Left {playerOneScore} - {playerTwoScore} Right");
            }
        }
    }
}
