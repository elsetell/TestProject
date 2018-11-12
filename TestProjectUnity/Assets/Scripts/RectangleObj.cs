using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleObj : MonoBehaviour {
    GameLogic gLogic;

    public void CreateRect(GameLogic gl)
    {
        GetComponent<SpriteRenderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
        gLogic = gl;
    }
}
