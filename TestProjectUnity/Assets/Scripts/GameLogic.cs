using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {
    private Transform parentRect;
    private List<Transform> rectangles = new List<Transform>();

    //double click var
    private Vector2 lastClickPos;
    private float IntervalTouch;
    static float clickInterval = 0.3f;

    private ConnectObj connectNow;//not closed connect
    private RectangleObj rectNow;//choose rect

    private void Start()
    {
        parentRect = GameObject.Find("Rectangles").transform;
    }
    private void Update()
    {
        //work with rect
        if (Input.GetMouseButtonDown(0))
        {
            Transform hit = GetTransformHit(GetPositionMouse());
            if (DoubleClick())//DestroyRectangle
            {
                if (hit != null)
                    hit.GetComponent<RectangleObj>().DestroyRect();
            }
            else if (!CheckCollision(hit))//CreateRectangle
            {
                RectangleObj cloneRect = Instantiate(Resources.Load("Rect") as GameObject, GetPositionMouse(), Quaternion.identity, parentRect)
                    .GetComponent<RectangleObj>();
                cloneRect.CreateRect(this);
                rectangles.Add(cloneRect.transform);
            }
            else
            {
                GetInfoAboutLastClick();
            }
        }
        //work with connect(line)
        if (Input.GetMouseButtonDown(1))
        {
            Transform hit = GetTransformHit(GetPositionMouse());
            if (connectNow == null)//CreateConnect
            {
                if (hit == null) return;
                connectNow = Instantiate(Resources.Load("Connect") as GameObject).GetComponent<ConnectObj>();
                connectNow.CreateConnect(hit, this);
            }
            else//Finish CreateConnect
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

    public Transform GetTransformHit(Vector2 posTo)
    {
        return Physics2D.Raycast(posTo, Vector2.zero).transform;
    }

    bool DoubleClick()
    {
        return (Time.time < IntervalTouch + clickInterval && lastClickPos == GetPositionMouse());
    }

    public bool CheckCollision(Transform hit) // check foк obstacles(rectangle)
    {
        return (hit != null && (hit.tag == "Obstacle")) ? true : false;
    }
}
