using UnityEngine;

namespace Summons.Scripts.ViewCtrls.MiniGames.BallGame
{
    public class PlayerShadow : MonoBehaviour
    {
        public GameObject shadow;

        private void Start()
        {
            LoadShadow();
        }

        private void Update()
        {
            LoadShadow();
        }

        private void LoadShadow()
        {
            if (!shadow) return;
            // 获取纹理并传递到shader
            var shadowMat = shadow.GetComponent<SpriteRenderer>().material;
            var playerTex = GetComponent<SpriteRenderer>().sprite.texture;
            shadowMat.SetTexture("_PlayerTex", playerTex);
        }
    }
}