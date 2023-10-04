using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBit : MonoBehaviour
{
    public float lifeTime = 3;
    public float moveSpd = 0.1f;
    
    public int dirX = 0;
    public int dirY = 0;

    float time = 0;

    private void Update()
    {
        Vector2 pos = transform.position;
        pos.x += moveSpd * dirX / normalized();
        pos.y += moveSpd * dirY / normalized();
        transform.position = pos;

        time += Time.deltaTime;

        if (time >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    float normalized()
    {
        if (dirX != 0 && dirY != 0)
        {
            return Mathf.Sqrt(2);
        }
        else
        {
            return 1;
        }
    }
}
