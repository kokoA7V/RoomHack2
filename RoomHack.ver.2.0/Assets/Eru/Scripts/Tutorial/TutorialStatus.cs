using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialStatus : MonoBehaviour
{
    private Text hrNumbar;

    void Start()
    {
        hrNumbar = GetComponent<Text>();
        hrNumbar.text = Random.Range(70, 91).ToString();
        StartCoroutine(HR());
    }

    private IEnumerator HR()
    {
        yield return new WaitForSeconds(1f);

        hrNumbar.text = Random.Range(70, 91).ToString();

        StartCoroutine(HR());
        yield break;
    }
}
