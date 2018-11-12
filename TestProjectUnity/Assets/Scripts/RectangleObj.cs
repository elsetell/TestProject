using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleObj : MonoBehaviour {
    GameLogic gLogic;
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

    }
    private void OnMouseDown()//endDrag = !connectmove
    {

    }

    private void OnMouseDrag()//moveRect
    {
        _transform.position = gLogic.GetPositionMouse();
    }

    public void DestroyRect() //DestroyRect and his connects
    {
        Destroy(gameObject);
    }
}
