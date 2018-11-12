using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectObj : MonoBehaviour {
    private GameLogic gLogic;
    private LineRenderer lr;
    [HideInInspector] public Transform start;
    [HideInInspector] public Transform end;
    [HideInInspector] public Vector2 endVector;
    public bool move;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (move)
        {
            RefreshPosition();
        }
    }

    private void RefreshPosition()
    {
        lr.SetPosition(0, start.position);
        if (end != null)
            lr.SetPosition(1, end.position);
        else
            lr.SetPosition(1, gLogic.GetPositionMouse());
    }

    public void CreateConnect(Transform target, GameLogic gl)
    {
        start = target;
        start.GetComponent<RectangleObj>().connectsRect.Add(this);
        gLogic = gl;
    }
    public void CloseConnect(Transform target)
    {
        if (target == null ||  target == start)
        {
            DestroyConnect();
            return;
        }
        end = target;
        end.GetComponent<RectangleObj>().connectsRect.Add(this);
        RefreshPosition();
        move = false;
    }

    public void DestroyConnect()
    {
        start.GetComponent<RectangleObj>().connectsRect.Remove(this);
        if (end)
            end.GetComponent<RectangleObj>().connectsRect.Remove(this);
        Destroy(gameObject);
    }

    public void MoveEnd(Vector2 to)
    {
        endVector = to;
    }
}
