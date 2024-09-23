using DG.Tweening;
using UnityEngine;

// 引入DOTween的命名空间

namespace UI
{
    public class FloatingEffect : MonoBehaviour
    {
        // 漂浮的高度和速度
        public float floatHeight = 0.5f; // 上下浮动的高度
        public float floatDuration = 2f; // 完成一个浮动的时间

        private Vector3 originalPosition; // 原始位置

        void Start()
        {
            // 记录物体的初始位置
            originalPosition = transform.position;

            // 开始浮动效果
            StartFloating();
        }

        void StartFloating()
        {
            // 使用DOMoveY实现物体在Y轴上来回浮动
            transform.DOMoveY(originalPosition.y + floatHeight, floatDuration)
                .SetEase(Ease.InOutSine) // 设置缓动效果为平滑的Sine曲线
                .SetLoops(-1, LoopType.Yoyo); // 无限循环，Yoyo效果来回切换
        }
    }
}