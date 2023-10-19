using System.Collections;
using UnityEngine;

public class TutorialMateController : MonoBehaviour
{
    public enum MOVE_ROOM
    {
        LeftRoom,
        CenterRoom,
        RightRoom,
    }
    [HideInInspector]
    public MOVE_ROOM moveRoom;

    [HideInInspector]
    public bool moveStartFlg = false;

    [HideInInspector]
    public TutorialManager tutorialManager;

    [SerializeField]
    private GameObject map, bulletPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && moveStartFlg)
        {
            
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (moveRoom.ToString() != hit.collider.gameObject.name || hit.collider == null) return;

            moveStartFlg = false;
            if (moveRoom == MOVE_ROOM.LeftRoom) StartCoroutine(MoveLeftRoom());
            else if (moveRoom == MOVE_ROOM.CenterRoom) StartCoroutine(MoveCenterRoom());
            else if (moveRoom == MOVE_ROOM.RightRoom) StartCoroutine(MoveRightRoom());
        }
    }

    public IEnumerator Enemy1Kill()
    {
        float currentRotation = 0.0f;
        while (currentRotation < 30)
        {
            float rotationStep = 180 * Time.deltaTime;

            transform.Rotate(Vector3.forward, rotationStep);
            currentRotation += rotationStep;

            yield return null;
        }

        for(int i = 0; i < 3; i++)
        {
            //弾を打つ
            AudioPlay.instance.SEPlay(5);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            Vector3 shootDirection = Quaternion.Euler(0, 0, transform.eulerAngles.z) * Vector3.up;
            rb.velocity = shootDirection * 3f;
            bullet.transform.up = shootDirection;
            yield return new WaitForSeconds(0.3f);
        }

        tutorialManager.questFlg = true;
        yield break;
    }


    public IEnumerator Enemy2Kill()
    {
        for (int i = 0; i < 3; i++)
        {
            //弾を打つ
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            Vector3 shootDirection = Quaternion.Euler(0, 0, transform.eulerAngles.z) * Vector3.up;
            rb.velocity = shootDirection * 3f;
            bullet.transform.up = shootDirection;
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(0.3f);

        float currentRotation = 0.0f;
        while (currentRotation < 50)
        {
            float rotationStep = 180 * Time.deltaTime;

            transform.Rotate(Vector3.forward, rotationStep);
            currentRotation += rotationStep;

            yield return null;
        }

        for (int i = 0; i < 3; i++)
        {
            //弾を打つ
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            Vector3 shootDirection = Quaternion.Euler(0, 0, transform.eulerAngles.z) * Vector3.up;
            rb.velocity = shootDirection * 3f;
            bullet.transform.up = shootDirection;
            yield return new WaitForSeconds(0.3f);
        }

        tutorialManager.questFlg = true;
        yield break;
    }


    private IEnumerator MoveLeftRoom()
    {
        float currentRotation = 0.0f;
        while (currentRotation < 90)
        {
            float rotationStep = 180 * Time.deltaTime;

            transform.Rotate(Vector3.forward, rotationStep);
            currentRotation += rotationStep;

            yield return null;
        }

        float journeyLength = Vector3.Distance(map.transform.position, new Vector3(1.84f, map.transform.position.y, map.transform.position.z));
        float startTime = Time.time;

        while (map.transform.position.x <= 1.83f)
        {
            float distanceCovered = (Time.time - startTime) * 0.25f;
            float fractionOfJourney = distanceCovered / journeyLength;

            map.transform.position = Vector3.Lerp(map.transform.position, new Vector3(1.84f, map.transform.position.y, map.transform.position.z), fractionOfJourney);

            yield return null;
        }

        tutorialManager.leftRoomSR.color = new Color(tutorialManager.leftRoomSR.color.r, tutorialManager.leftRoomSR.color.g, tutorialManager.leftRoomSR.color.b, 0f);
        tutorialManager.questFlg = true;
        yield break;
    }

    private IEnumerator MoveCenterRoom()
    {
        float currentRotation = 0.0f;
        while (currentRotation < 150)
        {
            float rotationStep = 180 * Time.deltaTime;

            transform.Rotate(Vector3.forward, rotationStep);
            currentRotation += rotationStep;

            yield return null;
        }

        float journeyLength = Vector3.Distance(map.transform.position, new Vector3(-4.54f, map.transform.position.y, map.transform.position.z));
        float startTime = Time.time;

        while (map.transform.position.x >= -4.53f)
        {
            float distanceCovered = (Time.time - startTime) * 0.25f;
            float fractionOfJourney = distanceCovered / journeyLength;

            map.transform.position = Vector3.Lerp(map.transform.position, new Vector3(-4.54f, map.transform.position.y, map.transform.position.z), fractionOfJourney);

            yield return null;
        }

        tutorialManager.centerRoomSR.color = new Color(tutorialManager.centerRoomSR.color.r, tutorialManager.centerRoomSR.color.g, tutorialManager.centerRoomSR.color.b, 0f);
        tutorialManager.questFlg = true;
        yield break;
    }

    private IEnumerator MoveRightRoom()
    {
        float journeyLength = Vector3.Distance(map.transform.position, new Vector3(-6.44f, map.transform.position.y, map.transform.position.z));
        float startTime = Time.time;

        while (map.transform.position.x >= -6.43f)
        {
            float distanceCovered = (Time.time - startTime) * 0.25f;
            float fractionOfJourney = distanceCovered / journeyLength;

            map.transform.position = Vector3.Lerp(map.transform.position, new Vector3(-6.44f, map.transform.position.y, map.transform.position.z), fractionOfJourney);

            yield return null;
        }

        tutorialManager.rightRoomSR.color = new Color(tutorialManager.rightRoomSR.color.r, tutorialManager.rightRoomSR.color.g, tutorialManager.rightRoomSR.color.b, 0f);
        //敵の殲滅
        StartCoroutine(Enemy2Kill());

        yield break;
    }
}
