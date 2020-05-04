using UnityEngine;

namespace IdleTransport.Utilities.DynamicScrollList
{
    public interface IScrollItem
    {
        void Reset();
        int CurrentIndex { get; set; }
        RectTransform RectTransform { get; }
    }
}
