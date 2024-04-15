using System;
using System.Collections.Generic;
using Summons.Scripts.Models;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Summons.Scripts.ViewCtrls.MiniGames
{
    public class Ggl : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler,IMiniGameCtrl
    {
        //是否擦除了
        public bool isStartEraser;

        //是否擦除结束了
        public bool isEndEraser;
        public RawImage uiTex;

        [Header("笔刷大小")] public int brushSize = 50;

        [Header("刮刮乐比例")] public int rate = 90;

        private float colorA;

        private readonly float distance = 1f;

        //结束事件
        public Action eraserEndEvent;

        //开始事件
        public Action eraserStartEvent;
        private double fate;
        private Vector2 lastPos; //最后一个点
        private float maxColorA;
        private int mHeight;
        private int mWidth;
        private Texture2D MyTex;
        private Vector2 penultPos; //倒数第二个点
        private readonly float radius = 12f;
        private Action _onComplete;
        [SerializeField] private TMP_Text questInfoText;
        // private bool startDraw;


        private Texture2D tex;
        private bool twoPoints;

        private void Awake()
        {
            tex = (Texture2D)uiTex.mainTexture;
            MyTex = new Texture2D(tex.width, tex.height, TextureFormat.ARGB32, false);
            mWidth = MyTex.width;
            mHeight = MyTex.height;
            MyTex.SetPixels(tex.GetPixels());
            MyTex.Apply();
            uiTex.texture = MyTex;
            maxColorA = MyTex.GetPixels().Length;
            colorA = 0;
            isEndEraser = false;
            isStartEraser = false;
        }

        /// <summary>
        ///     贝塞尔平滑
        /// </summary>
        /// <param name="start">起点</param>
        /// <param name="mid">中点</param>
        /// <param name="end">终点</param>
        /// <param name="segments">段数</param>
        /// <returns></returns>
        public Vector2[] Beizier(Vector2 start, Vector2 mid, Vector2 end, int segments)
        {
            var d = 1f / segments;
            var points = new Vector2[segments - 1];
            for (var i = 0; i < points.Length; i++)
            {
                var t = d * (i + 1);
                points[i] = (1 - t) * (1 - t) * mid + 2 * t * (1 - t) * start + t * t * end;
            }

            var rps = new List<Vector2>();
            rps.Add(mid);
            rps.AddRange(points);
            rps.Add(end);
            return rps.ToArray();
        }

        private void CheckPoint(Vector3 pScreenPos)
        {
            var worldPos = Camera.main.ScreenToWorldPoint(pScreenPos);
            var localPos = uiTex.gameObject.transform.InverseTransformPoint(worldPos);

            if (localPos.x > -mWidth / 2 && localPos.x < mWidth / 2 && localPos.y > -mHeight / 2 &&
                localPos.y < mHeight / 2)
            {
                for (var i = (int)localPos.x - brushSize; i < (int)localPos.x + brushSize; i++)
                for (var j = (int)localPos.y - brushSize; j < (int)localPos.y + brushSize; j++)
                {
                    if (Mathf.Pow(i - localPos.x, 2) + Mathf.Pow(j - localPos.y, 2) > Mathf.Pow(brushSize, 2))
                        continue;
                    if (i < 0)
                        if (i < -mWidth / 2)
                            continue;
                    if (i > 0)
                        if (i > mWidth / 2)
                            continue;
                    if (j < 0)
                        if (j < -mHeight / 2)
                            continue;
                    if (j > 0)
                        if (j > mHeight / 2)
                            continue;

                    var col = MyTex.GetPixel(i + mWidth / 2, j + mHeight / 2);
                    if (col.a != 0f)
                    {
                        col.a = 0.0f;
                        colorA++;
                        MyTex.SetPixel(i + mWidth / 2, j + mHeight / 2, col);
                    }
                }

                //开始刮的时候 去判断进度
                if (!isStartEraser)
                {
                    isStartEraser = true;
                    InvokeRepeating("getTransparentPercent", 0f, 0.2f);
                    if (eraserStartEvent != null)
                        eraserStartEvent.Invoke();
                }

                MyTex.Apply();
            }
        }

        /// <summary>
        ///     检测当前刮刮卡 进度
        /// </summary>
        /// <returns></returns>
        public void getTransparentPercent()
        {
            if (isEndEraser) return;
            fate = colorA / maxColorA * 100;
            fate = (float)Math.Round(fate, 2);
            if (fate >= rate)
            {
                isEndEraser = true;
                //CancelInvoke("getTransparentPercent");
                // Debug.Log("挑战成功");

                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                Debug.Log("小游戏完成了");
                _onComplete();
                uiTex.gameObject.SetActive(false);
                //触发结束事件
                if (eraserEndEvent != null)
                    eraserEndEvent.Invoke();
            }
        }
        public void Setup(QuestArgs args, Action onComplete)
        {
            questInfoText.text = args.GetType().Name;
            _onComplete = onComplete;
        }

        #region 事件

        public void OnPointerDown(PointerEventData eventData)
        {
            if (isEndEraser) return;
            // startDraw = true;
            penultPos = eventData.position;
            CheckPoint(penultPos);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (isEndEraser) return;
            if (twoPoints && Vector2.Distance(eventData.position, lastPos) > distance) //如果两次记录的鼠标坐标距离大于一定的距离，开始记录鼠标的点
            {
                var pos = eventData.position;
                var dis = Vector2.Distance(lastPos, pos);

                CheckPoint(eventData.position);
                var segments = (int)(dis / radius); //计算出平滑的段数                                              
                segments = segments < 1 ? 1 : segments;
                if (segments >= 10) segments = 10;
                var points = Beizier(penultPos, lastPos, pos, segments); //进行贝塞尔平滑
                for (var i = 0; i < points.Length; i++) CheckPoint(points[i]);
                lastPos = pos;
                if (points.Length > 2)
                    penultPos = points[points.Length - 2];
            }
            else
            {
                twoPoints = true;
                lastPos = eventData.position;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (isEndEraser) return;
            //CheckPoint(eventData.position);
            // startDraw = false;
            twoPoints = false;
        }

        #endregion
    }
}