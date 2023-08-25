using UnityEngine;

public class HackManager : MonoBehaviour
{
    public GameObject HackUIObj;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.TryGetComponent<IUnitHack>(out IUnitHack iUnitHack))
            {
                // クリック処理
                if (HackUIObj == null) return;
                GameObject hackUIObj = Instantiate(HackUIObj);
                HackUI hackUI = hackUIObj.GetComponent<HackUI>();

                //カメラ
                if (hit.collider.gameObject.TryGetComponent<CameraController>(out CameraController cameraCon))
                {
                    hackUI._randomFlg = cameraCon.randomFlg;
                    hackUI._word = new string[cameraCon.word.Length];
                    for (int i = 0; i < cameraCon.word.Length; i++) hackUI._word[i] = cameraCon.word[i];
                }

                //ドア
                else if (hit.collider.gameObject.TryGetComponent<DoorController>(out DoorController doorCon))
                {
                    hackUI._randomFlg = doorCon.randomFlg;
                    hackUI._word = new string[cameraCon.word.Length];
                    for (int i = 0; i < doorCon.word.Length; i++) hackUI._word[i] = doorCon.word[i];
                }
            }
        }
    }
}