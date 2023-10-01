using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public TutorialText tutorialText;

    public TutorialHackManager tutorialHackManager;

    public TutorialDoor tutorialDoor;

    public TutorialMateController tutorialMate;


    public TutorialEnemy tutorialEnemy;

    public TutorialEnemy2 tutorialEnemy1, tutorialEnemy2;

    public bool questFlg = false;

    [HideInInspector]
    public Image hackButton;

    [HideInInspector]
    public Text typingText;

    [HideInInspector]
    public int j = 0;

    [HideInInspector]
    public bool q1 = false;

    [HideInInspector]
    public bool q2 = false;

    [HideInInspector]
    public bool typingStartFlg = false;

    [HideInInspector]
    public bool buttonFlg = false;

    [SerializeField]
    private Image mapWindow, statusWindow, hackWindow;

    [SerializeField]
    private SpriteRenderer mateSR, rightDoor1SR, rightDoor2SR, leftDoor1SR, leftDoor2SR, cameraSR;

    [SerializeField]
    private Text timeText;

    [SerializeField]
    private SpriteRenderer enemy1SR, enemy2SR, enemy3SR;

    public SpriteRenderer leftRoomSR, centerRoomSR, rightRoomSR;

    private readonly int[] stopNo = new int[] { 8, 9, 10, 11, 14, 15, 16, 17, 19, 22, 24, 26, 27, 30, 33, 34, 37, 41, 43, 44, 999};


    private readonly float blinkTime = 0.1f;

    void Start()
    {
        tutorialHackManager.nowTypingFlg = true;
        tutorialHackManager.tutorialManager = GetComponent<TutorialManager>();

        leftRoomSR.color = new Color(leftRoomSR.color.r, leftRoomSR.color.g, leftRoomSR.color.b, 0f);
        centerRoomSR.color = new Color(centerRoomSR.color.r, centerRoomSR.color.g, centerRoomSR.color.b, 0f);
        rightRoomSR.color = new Color(rightRoomSR.color.r, rightRoomSR.color.g, rightRoomSR.color.b, 0f);

        q1 = false;
        q2 = false;
    }

    void Update()
    {
        if (tutorialText.clearFlg)
        {
            TutorialClear();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) tutorialText.clearFlg = true;

        if (tutorialText.i == stopNo[j])
        {
            j++;
            tutorialText.stopFlg = true;

            //やってほしいこと
            StartCoroutine(WindowBlink());
        }

        if(questFlg && tutorialText.stopFlg)
        {
            //tutorialText.textFlg = false;
            //StartCoroutine(tutorialText.Dialogue());
            questFlg = false;
            tutorialText.stopFlg = false;
            buttonFlg = false;
        }

        if(q1 && q2 && j == 18)
        {
            q1 = false;
            q2 = false;
            questFlg = true;
        }
    }

    private void TutorialClear()
    {
        GameData.tutorial = true;
        Load.SL = 2;
        SceneManager.LoadScene("LoadScene");
    }

    private IEnumerator WindowBlink()
    {
        if(j == 1)
        {
            //マップウィンドウを点滅
            for(int k = 0; k < 3; k++)
            {
                yield return new WaitForSeconds(blinkTime);
                mapWindow.color = new Color(mapWindow.color.r, mapWindow.color.g, mapWindow.color.b, 0f);
                yield return new WaitForSeconds(blinkTime);
                mapWindow.color = new Color(mapWindow.color.r, mapWindow.color.g, mapWindow.color.b, 1f);
            }

            questFlg = true;
        }
        else if(j == 2)
        {
            //右のウィンドウを点滅
            for (int k = 0; k < 3; k++)
            {
                yield return new WaitForSeconds(blinkTime);
                statusWindow.color = new Color(statusWindow.color.r, statusWindow.color.g, statusWindow.color.b, 0f);
                hackWindow.color = new Color(hackWindow.color.r, hackWindow.color.g, hackWindow.color.b, 0f);
                yield return new WaitForSeconds(blinkTime);
                statusWindow.color = new Color(statusWindow.color.r, statusWindow.color.g, statusWindow.color.b, 1f);
                hackWindow.color = new Color(hackWindow.color.r, hackWindow.color.g, hackWindow.color.b, 1f);
            }

            questFlg = true;
        }
        else if (j == 3)
        {
            //味方を点滅
            for (int k = 0; k < 3; k++)
            {
                yield return new WaitForSeconds(blinkTime);
                mateSR.color = new Color(mateSR.color.r, mateSR.color.g, mateSR.color.b, 0f);
                yield return new WaitForSeconds(blinkTime);
                mateSR.color = new Color(mateSR.color.r, mateSR.color.g, mateSR.color.b, 1f);
            }

            questFlg = true;
        }
        else if (j == 4)
        {
            //敵及び敵の管理下のものを点滅
            for (int k = 0; k < 3; k++)
            {
                yield return new WaitForSeconds(blinkTime);
                leftDoor1SR.color = new Color(leftDoor1SR.color.r, leftDoor1SR.color.g, leftDoor1SR.color.b, 0f);
                leftDoor2SR.color = new Color(leftDoor2SR.color.r, leftDoor2SR.color.g, leftDoor2SR.color.b, 0f);
                rightDoor1SR.color = new Color(rightDoor1SR.color.r, rightDoor1SR.color.g, rightDoor1SR.color.b, 0f);
                rightDoor2SR.color = new Color(rightDoor2SR.color.r, rightDoor2SR.color.g, rightDoor2SR.color.b, 0f);
                cameraSR.color = new Color(cameraSR.color.r, cameraSR.color.g, cameraSR.color.b, 0f);
                yield return new WaitForSeconds(blinkTime);
                leftDoor1SR.color = new Color(leftDoor1SR.color.r, leftDoor1SR.color.g, leftDoor1SR.color.b, 1f);
                leftDoor2SR.color = new Color(leftDoor2SR.color.r, leftDoor2SR.color.g, leftDoor2SR.color.b, 1f);
                rightDoor1SR.color = new Color(rightDoor1SR.color.r, rightDoor1SR.color.g, rightDoor1SR.color.b, 1f);
                rightDoor2SR.color = new Color(rightDoor2SR.color.r, rightDoor2SR.color.g, rightDoor2SR.color.b, 1f);
                cameraSR.color = new Color(cameraSR.color.r, cameraSR.color.g, cameraSR.color.b, 1f);
            }

            questFlg = true;
        }
        else if (j == 5)
        {
            //左のドアを点滅
            for (int k = 0; k < 3; k++)
            {
                yield return new WaitForSeconds(blinkTime);
                leftDoor1SR.color = new Color(leftDoor1SR.color.r, leftDoor1SR.color.g, leftDoor1SR.color.b, 0f);
                leftDoor2SR.color = new Color(leftDoor2SR.color.r, leftDoor2SR.color.g, leftDoor2SR.color.b, 0f);
                yield return new WaitForSeconds(blinkTime);
                leftDoor1SR.color = new Color(leftDoor1SR.color.r, leftDoor1SR.color.g, leftDoor1SR.color.b, 1f);
                leftDoor2SR.color = new Color(leftDoor2SR.color.r, leftDoor2SR.color.g, leftDoor2SR.color.b, 1f);
            }

            tutorialHackManager.nowTypingFlg = false;
            tutorialHackManager.hackType = TutorialHackManager.HACK_TYPE.Door1;
        }
        else if (j == 6)
        {
            //ハックウィンドウを点滅
            for (int k = 0; k < 3; k++)
            {
                yield return new WaitForSeconds(blinkTime);
                hackWindow.color = new Color(hackWindow.color.r, hackWindow.color.g, hackWindow.color.b, 0f);
                yield return new WaitForSeconds(blinkTime);
                hackWindow.color = new Color(hackWindow.color.r, hackWindow.color.g, hackWindow.color.b, 1f);
            }

            questFlg = true;
        }
        else if (j == 7)
        {
            //ハックボタンを点滅
            for (int k = 0; k < 3; k++)
            {
                yield return new WaitForSeconds(blinkTime);
                hackButton.color = new Color(hackButton.color.r, hackButton.color.g, hackButton.color.b, 0f);
                yield return new WaitForSeconds(blinkTime);
                hackButton.color = new Color(hackButton.color.r, hackButton.color.g, hackButton.color.b, 1f);
            }

            buttonFlg = true;
        }
        else if (j == 8)
        {
            //文字を点滅
            for (int k = 0; k < 3; k++)
            {
                yield return new WaitForSeconds(blinkTime);
                typingText.color = new Color(typingText.color.r, typingText.color.g, typingText.color.b, 0f);
                yield return new WaitForSeconds(blinkTime);
                typingText.color = new Color(typingText.color.r, typingText.color.g, typingText.color.b, 1f);
            }

            questFlg = true;
        }
        else if (j == 9)
        {
            //時間を点滅
            for (int k = 0; k < 3; k++)
            {
                yield return new WaitForSeconds(blinkTime);
                timeText.color = new Color(timeText.color.r, timeText.color.g, timeText.color.b, 0f);
                yield return new WaitForSeconds(blinkTime);
                timeText.color = new Color(timeText.color.r, timeText.color.g, timeText.color.b, 1f);
            }

            typingStartFlg = true;
            
        }
        else if (j == 10)
        {
            //ハックボタンを点滅
            for (int k = 0; k < 3; k++)
            {
                yield return new WaitForSeconds(blinkTime);
                hackButton.color = new Color(hackButton.color.r, hackButton.color.g, hackButton.color.b, 0f);
                yield return new WaitForSeconds(blinkTime);
                hackButton.color = new Color(hackButton.color.r, hackButton.color.g, hackButton.color.b, 1f);
            }

            buttonFlg = true;
        }
        else if (j == 11)
        {
            //左の部屋を点滅
            for (int k = 0; k < 3; k++)
            {
                yield return new WaitForSeconds(blinkTime);
                leftRoomSR.color = new Color(leftRoomSR.color.r, leftRoomSR.color.g, leftRoomSR.color.b, 0f);
                yield return new WaitForSeconds(blinkTime);
                leftRoomSR.color = new Color(leftRoomSR.color.r, leftRoomSR.color.g, leftRoomSR.color.b, 1f);
            }

            tutorialMate.tutorialManager = GetComponent<TutorialManager>();
            tutorialMate.moveRoom = TutorialMateController.MOVE_ROOM.LeftRoom;
            tutorialMate.moveStartFlg = true;
        }
        else if (j == 12)
        {
            //敵を点滅
            for (int k = 0; k < 3; k++)
            {
                yield return new WaitForSeconds(blinkTime);
                enemy1SR.color = new Color(enemy1SR.color.r, enemy1SR.color.g, enemy1SR.color.b, 0f);
                yield return new WaitForSeconds(blinkTime);
                enemy1SR.color = new Color(enemy1SR.color.r, enemy1SR.color.g, enemy1SR.color.b, 1f);
            }

            questFlg = true;
        }
        else if( j == 13)
        {
            //ステータスウィンドウを点滅
            for (int k = 0; k < 3; k++)
            {
                yield return new WaitForSeconds(blinkTime);
                statusWindow.color = new Color(statusWindow.color.r, statusWindow.color.g, statusWindow.color.b, 0f);
                yield return new WaitForSeconds(blinkTime);
                statusWindow.color = new Color(statusWindow.color.r, statusWindow.color.g, statusWindow.color.b, 1f);
            }

            tutorialMate.tutorialManager = GetComponent<TutorialManager>();
            tutorialEnemy.tutorialManager = GetComponent<TutorialManager>();
            StartCoroutine(tutorialMate.Enemy1Kill());
            StartCoroutine(tutorialEnemy.EnemyRotate(-60));
        }
        else if (j == 14)
        {
            //中央の部屋を点滅
            for (int k = 0; k < 3; k++)
            {
                yield return new WaitForSeconds(blinkTime);
                centerRoomSR.color = new Color(centerRoomSR.color.r, centerRoomSR.color.g, centerRoomSR.color.b, 0f);
                yield return new WaitForSeconds(blinkTime);
                centerRoomSR.color = new Color(centerRoomSR.color.r, centerRoomSR.color.g, centerRoomSR.color.b, 1f);
            }

            centerRoomSR.color = new Color(centerRoomSR.color.r, centerRoomSR.color.g, centerRoomSR.color.b, 0f);
            tutorialDoor.tutorialManager = GetComponent<TutorialManager>();
            tutorialDoor.DoorClause();
            tutorialHackManager.nowObj = null;
        }
        else if (j == 15)
        {
            //左のドアを点滅
            for (int k = 0; k < 3; k++)
            {
                yield return new WaitForSeconds(blinkTime);
                leftDoor1SR.color = new Color(leftDoor1SR.color.r, leftDoor1SR.color.g, leftDoor1SR.color.b, 0f);
                leftDoor2SR.color = new Color(leftDoor2SR.color.r, leftDoor2SR.color.g, leftDoor2SR.color.b, 0f);
                yield return new WaitForSeconds(blinkTime);
                leftDoor1SR.color = new Color(leftDoor1SR.color.r, leftDoor1SR.color.g, leftDoor1SR.color.b, 1f);
                leftDoor2SR.color = new Color(leftDoor2SR.color.r, leftDoor2SR.color.g, leftDoor2SR.color.b, 1f);
            }

            tutorialHackManager.nowTypingFlg = false;
            buttonFlg = true;
            tutorialHackManager.hackType = TutorialHackManager.HACK_TYPE.Door1;
        }
        else if (j == 16)
        {
            //中央の部屋を点滅
            for (int k = 0; k < 3; k++)
            {
                yield return new WaitForSeconds(blinkTime);
                centerRoomSR.color = new Color(centerRoomSR.color.r, centerRoomSR.color.g, centerRoomSR.color.b, 0f);
                yield return new WaitForSeconds(blinkTime);
                centerRoomSR.color = new Color(centerRoomSR.color.r, centerRoomSR.color.g, centerRoomSR.color.b, 1f);
            }

            tutorialMate.tutorialManager = GetComponent<TutorialManager>();
            tutorialMate.moveRoom = TutorialMateController.MOVE_ROOM.CenterRoom;
            tutorialMate.moveStartFlg = true;
        }
        else if (j == 17)
        {
            //カメラを点滅
            for (int k = 0; k < 3; k++)
            {
                yield return new WaitForSeconds(blinkTime);
                cameraSR.color = new Color(cameraSR.color.r, cameraSR.color.g, cameraSR.color.b, 0f);
                yield return new WaitForSeconds(blinkTime);
                cameraSR.color = new Color(cameraSR.color.r, cameraSR.color.g, cameraSR.color.b, 1f);
            }

            buttonFlg = true;
            tutorialHackManager.hackType = TutorialHackManager.HACK_TYPE.Camera1;
        }
        else if (j == 18)
        {
            //敵を点滅
            for (int k = 0; k < 3; k++)
            {
                yield return new WaitForSeconds(blinkTime);
                enemy2SR.color = new Color(enemy2SR.color.r, enemy2SR.color.g, enemy2SR.color.b, 0f);
                enemy3SR.color = new Color(enemy3SR.color.r, enemy3SR.color.g, enemy3SR.color.b, 0f);
                yield return new WaitForSeconds(blinkTime);
                enemy2SR.color = new Color(enemy2SR.color.r, enemy2SR.color.g, enemy2SR.color.b, 1f);
                enemy3SR.color = new Color(enemy3SR.color.r, enemy3SR.color.g, enemy3SR.color.b, 1f);
            }

            tutorialEnemy1.tutorialManager = GetComponent<TutorialManager>();
            tutorialEnemy2.tutorialManager = GetComponent<TutorialManager>();
            buttonFlg = true;
            tutorialHackManager.hackType = TutorialHackManager.HACK_TYPE.Enemy2;
        }
        else if (j == 19)
        {
            //右のドアを点滅
            for (int k = 0; k < 3; k++)
            {
                yield return new WaitForSeconds(blinkTime);
                rightDoor1SR.color = new Color(rightDoor1SR.color.r, rightDoor1SR.color.g, rightDoor1SR.color.b, 0f);
                rightDoor2SR.color = new Color(rightDoor2SR.color.r, rightDoor2SR.color.g, rightDoor2SR.color.b, 0f);
                yield return new WaitForSeconds(blinkTime);
                rightDoor1SR.color = new Color(rightDoor1SR.color.r, rightDoor1SR.color.g, rightDoor1SR.color.b, 1f);
                rightDoor2SR.color = new Color(rightDoor2SR.color.r, rightDoor2SR.color.g, rightDoor2SR.color.b, 1f);
            }

            buttonFlg = true;
            tutorialHackManager.hackType = TutorialHackManager.HACK_TYPE.Door2;
        }
        else if (j == 20)
        {
            //右の部屋を点滅
            for (int k = 0; k < 3; k++)
            {
                yield return new WaitForSeconds(blinkTime);
                rightRoomSR.color = new Color(rightRoomSR.color.r, rightRoomSR.color.g, rightRoomSR.color.b, 0f);
                yield return new WaitForSeconds(blinkTime);
                rightRoomSR.color = new Color(rightRoomSR.color.r, rightRoomSR.color.g, rightRoomSR.color.b, 1f);
            }

            tutorialMate.tutorialManager = GetComponent<TutorialManager>();
            tutorialMate.moveRoom = TutorialMateController.MOVE_ROOM.RightRoom;
            tutorialMate.moveStartFlg = true;
        }

        yield break;
    }
}
