using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {
    private Transform parentRect;

    private void Start()
    {
        parentRect = GameObject.Find("Rectangles").transform;
    }
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Transform hit = GetTransformHit();
            RectangleObj cloneRect = Instantiate(Resources.Load("Rect") as GameObject, GetPositionMouse(), Quaternion.identity, parentRect)
                .GetComponent<RectangleObj>();
            cloneRect.CreateRect(this);
        }
        else
        {

        }
    }

    Vector2 GetPositionMouse()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    Transform GetTransformHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(GetPositionMouse(), Vector2.zero);
        return hit.transform;
    }
}
