using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {
    private Transform parentRect;

    //double click var
    private Vector2 lastClickPos;
    private float IntervalTouch;
    static float clickInterval = 0.3f;

    //line
    private ConnectObj connectNow;
    //rect
    private RectangleObj rectNow;

    private void Start()
    {
        parentRect = GameObject.Find("Rectangles").transform;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Transform hit = GetTransformHit();
            if (DoubleClick())//DestroyRectangle
            {
                if (hit != null && hit.tag == "Obstacle")
                    hit.GetComponent<RectangleObj>().DestroyRect();
            }
            else if (!CheckCollision(hit))//CreateRectangle
            {
                RectangleObj cloneRect = Instantiate(Resources.Load("Rect") as GameObject, GetPositionMouse(), Quaternion.identity, parentRect)
                    .GetComponent<RectangleObj>();
                cloneRect.CreateRect(this);
            }
            else
            {
                GetInfoAboutLastClick();
            }
        }
        //work with connect(line)
        if (Input.GetMouseButtonDown(1))
        {
            Transform hit = GetTransformHit();
            if (connectNow == null)//CreateConnect
            {
                if (hit == null) return;
                connectNow = Instantiate(Resources.Load("Connect") as GameObject).GetComponent<ConnectObj>();
                connectNow.CreateConnect(hit, this);
            }
            else//DoneCreateConnect
            {
                connectNow.CloseConnect(hit);
                connectNow = null;
            }
        }
    }


    void GetInfoAboutLastClick()
    {
        lastClickPos = GetPositionMouse();
        IntervalTouch = Time.time;
    }

    public Vector2 GetPositionMouse()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    Transform GetTransformHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(GetPositionMouse(), Vector2.zero);
        return hit.transform;
    }

    bool DoubleClick()
    {
        return (Time.time < IntervalTouch + clickInterval && lastClickPos == GetPositionMouse());
    }

    bool CheckCollision(Transform hit) // check foк obstacles(rectangle)
    {
        return (hit != null && (hit.tag == "Obstacle" || hit.tag == "AreaObstacle")) ? true : false;
    }
}
