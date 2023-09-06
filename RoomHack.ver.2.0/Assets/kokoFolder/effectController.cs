using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectController : MonoBehaviour
{
    [SerializeField]Vector2 startPos = new Vector2(0, 4);
    [SerializeField]Vector2 endPos = new Vector2(0, -4);
    [SerializeField]float moveSpd = 0.1f;

    private void Start()
    {
        transform.position = startPos;
    }

    private void Update()
    {
        Vector2 pos = transform.position;
        pos.y -= moveSpd;
        transform.position = pos;

        if(transform.position.y <= endPos.y)
        {
            transform.position = startPos;
        }
    }
}
