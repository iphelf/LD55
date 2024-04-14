using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerAnimation playerAnimation;
    public float moveSpeed = 5f;
    

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput) * moveSpeed * Time.deltaTime;
        Move(movement);
        Rotate(movement);
        
    }

    private void Move(Vector2 movement)
    {
        transform.Translate(movement);
        //if (movement.magnitude == 0) playerAnimation.PlayStayAnimation();
        //else playerAnimation.PlayRunAnimation();

    }

    private void Rotate(Vector2 movement)
    {
        if (movement.x > 0) spriteRenderer.flipX = true;
        if (movement.x < 0) spriteRenderer.flipX = false;

    }
}


