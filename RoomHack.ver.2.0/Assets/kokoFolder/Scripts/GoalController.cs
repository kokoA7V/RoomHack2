using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    [SerializeField]
    GameManager_Rescue gmr;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<MateController>(out MateController mc))
        {
            gmr.goalInObj.Add(mc);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<MateController>(out MateController mc))
        {
            gmr.goalInObj.Remove(mc);
        }
    }
}
