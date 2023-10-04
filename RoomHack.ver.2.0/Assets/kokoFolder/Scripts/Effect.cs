using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField ]GameObject EffectBit;

    public float lifeTimeTotal = 1;
    public float moveSpdTotal = 0.01f;

    private void Start()
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                EffectGenerate(lifeTimeTotal, moveSpdTotal, i, j);
            }
        }
    }

    void EffectGenerate(float lt, float spd, int dx, int dy)
    {
        GameObject obj = Instantiate(EffectBit, this.transform.position, Quaternion.identity);
        obj.transform.parent = this.transform;

        EffectBit EB = obj.GetComponent<EffectBit>();
        EB.lifeTime = lt;
        EB.moveSpd = spd;
        EB.dirX = dx;
        EB.dirY = dy;
    }
}
