using UnityEngine;

public class HackManager : MonoBehaviour
{
    public GameObject HackUIObj;
    public GameObject CoolHackUIObj;

    public bool nowTypingFlg = false;

    [HideInInspector]
    public GameObject nowObj;

    private GameObject nowHackUI;

    void Update()
    {
        if (nowTypingFlg) return;
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.TryGetComponent<IUnitHack>(out IUnitHack iUnitHack))
            {
                // クリック処理
                if (HackUIObj == null) return;
                if (nowObj == hit.collider.gameObject) return;
                nowObj = hit.collider.gameObject;
                Destroy(nowHackUI);

                if (iUnitHack.hacked)
                {
                    //CoolHackUIを生成
                    Debug.Log("CoolHackUIObj生成");
                    if (CoolHackUIObj == null) return;
                    nowHackUI = Instantiate(CoolHackUIObj);
                }
                else
                {
                    //HackUIを生成
                    nowHackUI = Instantiate(HackUIObj);
                    HackUI hackUI = nowHackUI.GetComponent<HackUI>();
                    hackUI.hackManager = GetComponent<HackManager>();
                    hackUI.unitHack = iUnitHack;

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
                        hackUI._word = new string[doorCon.word.Length];
                        for (int i = 0; i < doorCon.word.Length; i++) hackUI._word[i] = doorCon.word[i];
                    }
                }
            }
        }
    }
}