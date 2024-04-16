using Summons.Scripts.Managers;
using UnityEngine;

namespace Summons.Scripts.ViewCtrls.MiniGames.BoxGame
{
    public class Item : MonoBehaviour
    {
        public enum Type
        {
            Book,
            Cloth,
            Toy
        }

        [SerializeField] private Type type = Type.Book;
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private float gravityAddSpeed = 1f;

        public Vector3 Movement = new(0, 0, 0);
        public Vector3 StartPosition;
        private float GravityAddSpeed;
        private float movespeedX;

        private float movespeedY;

        // Start is called before the first frame update
        private void Start()
        {
            StartPosition = transform.position;
            Movement = StartPosition;
            movespeedX = moveSpeed;
        }

        // Update is called once per frame
        private void Update()
        {
            Move();
            //Debug.Log($"({movespeedX}, {movespeedY})");
            // Debug.Log(transform.position);
            //Debug.Log(transform.localPosition);
            movespeedY -= GravityAddSpeed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((type == Type.Book && other.CompareTag("BookBox")) || (type == Type.Cloth &&
                                                                       other.CompareTag("ClothBox"))
                                                                   || (type == Type.Toy && other.CompareTag("ToyBox")))
            {
                BoxGameCtrl.Instance.AddScore(1);
                AudioManager.PlaySfx(SfxKey.MiniGameScore);
            }

            movespeedX = moveSpeed;
            movespeedY = 0;
            GravityAddSpeed = 0;
            BoxGameCtrl.Instance.RestartBoxGame(this);
            gameObject.SetActive(false);
        }

        private void Move()
        {
            Movement.x += movespeedX * Time.deltaTime;
            Movement.y += movespeedY * Time.deltaTime;
            transform.position = new Vector3(Movement.x, Movement.y, 0);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AudioManager.PlaySfx(SfxKey.MiniGameClick);
                Rotate();
            }
            //if (moveY - Time.deltaTime >= 0)
        }

        private void Rotate()
        {
            movespeedX = 0;
            GravityAddSpeed = gravityAddSpeed;
        }

        public void ResetMoveSpeedX()
        {
            movespeedX = moveSpeed;
        }

        public void AddMoveSpeedX(float addspeed)
        {
            moveSpeed += addspeed;
            //ResetMoveSpeedX();
        }
    }
}