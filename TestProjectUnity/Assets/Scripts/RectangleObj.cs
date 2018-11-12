using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleObj : MonoBehaviour {
    GameLogic gLogic;
    public List<ConnectObj> connectsRect = new List<ConnectObj>();
    
    //transform
    private Transform _transform;
    public Vector2 sizeRect;
    public Vector2[] points;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        InitSize();
    }
    private void InitSize()
    {
        sizeRect = new Vector2(GetComponent<BoxCollider2D>().bounds.size.x/2, GetComponent<BoxCollider2D>().bounds.size.y/2);
        points[0] = new Vector2(-sizeRect.x, sizeRect.y);
        points[1] = new Vector2(-sizeRect.x, -sizeRect.y);
        points[2] = new Vector2(sizeRect.x, -sizeRect.y);
        points[3] = new Vector2(sizeRect.x, sizeRect.y);
    }
    public void CreateRect(GameLogic gl)
    {
        GetComponent<SpriteRenderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
        gLogic = gl;
    }

    private void OnMouseUp()//startDrag = connectmove
    {
        foreach (ConnectObj co in connectsRect)
            co.move = false;
    }
    private void OnMouseDown()//endDrag = !connectmove
    {
        foreach (ConnectObj co in connectsRect)
            co.move = true;
    }

    private void OnMouseDrag()//moveRect
    {
        if(!CheckCollisionAllCorners(points))
            _transform.position = gLogic.GetPositionMouse();
    }
    public bool CheckCollisionAllCorners(Vector2[] positions) // raycastAll(choose) vs check interval position
    {
        for (int i = 0; i < positions.Length; i++)
        {
            Transform hit = gLogic.GetTransformHit(gLogic.GetPositionMouse() + positions[i]);
            if (gLogic.CheckCollision(hit) && (hit != _transform && hit != _transform.GetChild(0)))
            {
                return true;
            }
        }
        return false;
    }

    public void DestroyRect() //DestroyRect and his connects
    {
        int count = connectsRect.Count;
        for (int i = count - 1; i >= 0; i--)
            connectsRect[i].DestroyConnect();
        Destroy(gameObject);
    }
}
