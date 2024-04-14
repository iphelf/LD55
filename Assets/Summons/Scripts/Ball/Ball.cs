using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float moveXMAX = 0.1f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float GravityAddSpeed = 0.5f;
    private float movespeedX;
    private float movespeedY;

    public Vector3 Movement=new Vector3(0,0,0);
    public Vector3 StartPosition;
    // Start is called before the first frame update
    void Start()
    {
        StartPosition=transform.position;
        Movement = StartPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Debug.Log($"({movespeedX}, {movespeedY})");
        // Debug.Log(transform.position);
        //Debug.Log(transform.localPosition);
        movespeedY -= GravityAddSpeed * Time.deltaTime;
    }

    private void Move()
    {
        Movement.x +=movespeedX * Time.deltaTime;
        Movement.y += movespeedY* Time.deltaTime;
        transform.position = new Vector3(Movement.x, Movement.y, 0);
        //if (moveY - Time.deltaTime >= 0)
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //moveX = (transform.position.x - player.transform.GetChild(0).position.x)*moveSpeed;
            if ((transform.position.x - player.transform.GetChild(0).position.x) * moveSpeed > moveXMAX)
                movespeedX = moveXMAX;
            else if ((transform.position.x - player.transform.GetChild(0).position.x) * moveSpeed < -moveXMAX)
                movespeedX = -moveXMAX;
            else movespeedX = (transform.position.x - player.transform.GetChild(0).position.x) * moveSpeed;
            movespeedY = -moveSpeed*movespeedY;
            //Debug.Log($"({11})");
            BallGameManager.Instance.AddScore(1);
        }
        if (other.CompareTag("Panel"))
        {
            BallGameManager.Instance.SubScore(1);
            Debug.Log("Set(False)");
            movespeedX = 0;
            movespeedY = 0;
            BallGameManager.Instance.RestartBallGame(this);
            gameObject.SetActive(false);
            //StartCoroutine(RestartBall());
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
    }

    IEnumerator RestartBall()
    {
        yield return new WaitForSeconds(2f);
    }
    public async Awaitable RestartBall1()
    {
        gameObject.SetActive(false);
        transform.position = StartPosition;
        //await WaitForSeconds(2f);
        gameObject.SetActive(true);
    }

}
