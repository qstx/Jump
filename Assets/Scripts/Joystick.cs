using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : ScrollRect
{
    protected float mRadius = 1f;
    public Vector2 vector;
    protected override void Start()
    {
        base.Start();
        mRadius = (transform as RectTransform).sizeDelta.x * 0.5f;
    }
    public override void OnDrag(PointerEventData eventData)
    {
        //Limit stick's position
        base.OnDrag(eventData);
        var contentPosition = this.content.anchoredPosition;
        if (contentPosition.magnitude > mRadius)
        {
            contentPosition = contentPosition.normalized * mRadius;
            SetContentAnchoredPosition(contentPosition);
        }
    }
    private void Update()
    {
        //Update movement direction
        vector = content.anchoredPosition / mRadius;
    }
}
