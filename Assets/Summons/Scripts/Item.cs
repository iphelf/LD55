using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Item : MonoBehaviour
{
    public enum Type
    {
        Book,
        Cloth,
        Toy
    };
    [SerializeField] Type type = Type.Book;
    [SerializeField] private float moveSpeed=1f;
    [SerializeField] private float gravityAddSpeed = 1f;
    private float GravityAddSpeed = 0f;
    private float movespeedX;
    private float movespeedY;

    public Vector3 Movement = new Vector3(0, 0, 0);
    public Vector3 StartPosition;
    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position;
        Movement = StartPosition;
        movespeedX = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //Debug.Log($"({movespeedX}, {movespeedY})");
        // Debug.Log(transform.position);
        //Debug.Log(transform.localPosition);
        movespeedY -= GravityAddSpeed * Time.deltaTime;
    }

    private void Move()
    {
        Movement.x += movespeedX * Time.deltaTime;
        Movement.y += movespeedY * Time.deltaTime;
        transform.position = new Vector3(Movement.x, Movement.y, 0);
        if (UnityEngine.Input.GetKeyDown(KeyCode.Space)) Rotate();
        //if (moveY - Time.deltaTime >= 0)
    }

    private void Rotate()
    {
        movespeedX = 0;
        GravityAddSpeed = gravityAddSpeed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (type == Type.Book && other.CompareTag("BookBox")|| type == Type.Cloth && other.CompareTag("ClothBox")
            || type == Type.Toy && other.CompareTag("ToyBox"))
        {
            
            BoxGameManager.Instance.AddScore(1);
        }
        movespeedX = moveSpeed;
        movespeedY = 0;
        GravityAddSpeed = 0;
        BoxGameManager.Instance.RestartBoxGame(this);
        gameObject.SetActive(false);
    }

    public void ResetMoveSpeedX()
    {
        movespeedX = moveSpeed;
    }
    public void AddMoveSpeedX(float addspeed)
    {
        moveSpeed += addspeed;
        ResetMoveSpeedX();
    }
}
