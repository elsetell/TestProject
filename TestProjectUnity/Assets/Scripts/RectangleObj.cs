using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleObj : MonoBehaviour {
    GameLogic gLogic;
    public List<ConnectObj> connectsRect = new List<ConnectObj>();
    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
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
        _transform.position = gLogic.GetPositionMouse();
    }

    public void DestroyRect() //DestroyRect and his connects
    {
        int count = connectsRect.Count;
        for (int i = count - 1; i >= 0; i--)
            connectsRect[i].DestroyConnect();
        Destroy(gameObject);
    }
}
