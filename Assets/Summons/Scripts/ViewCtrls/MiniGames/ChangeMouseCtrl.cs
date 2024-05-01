using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Summons.Scripts.ViewCtrls.MiniGames
{
    public class ChangeMouseCtrl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Texture2D cursorTexture;

        // 当鼠标指针进入对象时调用
        public void OnPointerEnter(PointerEventData eventData)
        {
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        private void OnDisable()
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}