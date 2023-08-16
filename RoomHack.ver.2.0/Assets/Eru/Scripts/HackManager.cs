using UnityEngine;

public class HackManager : MonoBehaviour
{
    public GameObject HackUIObj;
    public GameObject TypingObj;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.TryGetComponent<IUnitHack>(out IUnitHack iUnitHack))
            {
                // ÉNÉäÉbÉNèàóù
                if (HackUIObj == null) return;
                GameObject hackUI = Instantiate(HackUIObj);
            }
        }
    }

    public void TypingStart()
    {
        GameObject typing = Instantiate(TypingObj);
    }
}