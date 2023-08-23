using UnityEngine;

public class HackManager : MonoBehaviour
{
    public GameObject HackUIObj;

    private string[] word;
    private bool randomFlg;

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
                if(hit.collider.gameObject.TryGetComponent<CameraController>(out CameraController cameraCon))
                {
                    randomFlg = cameraCon.randomFlg;
                    for(int i = 0; i < cameraCon.word.Length; i++) word[i] = cameraCon.word[i];
                }

                //ドア
                else if (hit.collider.gameObject.TryGetComponent<DoorController>(out DoorController doorCon))
                {
                    randomFlg = doorCon.randomFlg;
                    for (int i = 0; i < doorCon.word.Length; i++) word[i] = doorCon.word[i];
                }

                //タイピングのデータを送る
                hackUI._randomFlg = randomFlg;
                for (int i = 0; i < word.Length; i++)
                {
                    hackUI._word[i] = word[i];
                }
            }
        }
    }
}