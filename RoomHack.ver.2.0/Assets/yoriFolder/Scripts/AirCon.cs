using System.Collections.Generic;
using UnityEngine;

public class AirCon : MonoBehaviour
{
    [SerializeField]
    private AirConditionerController airCon;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyController>(out EnemyController pc))
        {
            airCon = collision.GetComponent<AirConditionerController>();
            airCon.emList.Add(pc);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyController>(out EnemyController pc))
        {
            airCon = collision.GetComponent<AirConditionerController>();
            airCon.emList.Remove(pc);
            airCon.moveFlg = true;
        }
    }
}
