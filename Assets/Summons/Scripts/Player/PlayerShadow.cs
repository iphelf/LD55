using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    public GameObject shadow;
    void Start()
    {
       LoadShadow();
    }
    private void Update()
    {
       LoadShadow();
    }

    void LoadShadow()
    {
        if (!shadow)
        {
            return;
        }
        // 获取纹理并传递到shader
        var shadowMat = shadow.GetComponent<SpriteRenderer>().material;
        var playerTex = GetComponent<SpriteRenderer>().sprite.texture;
        shadowMat.SetTexture("_PlayerTex", playerTex);
    }
}
