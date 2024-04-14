using UnityEngine;

namespace Summons.Scripts.ViewCtrls.MiniGames.BallGame
{
    public class Player : MonoBehaviour
    {
        public float moveSpeed = 5f;
        private PlayerAnimation playerAnimation;
        private SpriteRenderer spriteRenderer;


        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }


        private void Update()
        {
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");
            var movement = new Vector2(horizontalInput, verticalInput) * moveSpeed * Time.deltaTime;
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
}