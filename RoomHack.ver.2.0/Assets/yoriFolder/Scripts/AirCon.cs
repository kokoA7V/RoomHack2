using UnityEngine;

public class AirCon : MonoBehaviour
{
    [SerializeField]
    private AirConditionerController airCon;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyController>(out EnemyController pc))
        {
            Debug.Log("Enemy追加" + collision.gameObject.name);
            airCon = airCon.GetComponent<AirConditionerController>();
            airCon.emList.Add(pc);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyController>(out EnemyController pc))
        {
            airCon = airCon.GetComponent<AirConditionerController>();
            airCon.emList.Remove(pc);
            Debug.Log("Enemy削除" + collision.gameObject.name);
            airCon.moveFlg = false;
        }
    }
}
