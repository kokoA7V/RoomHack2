using System.Collections;
using UnityEngine;

public class TutorialEnemy : MonoBehaviour
{
    [HideInInspector]
    public int hp = 3;

    [HideInInspector]
    public TutorialManager tutorialManager;

    void Update()
    {
        if (hp <= 0) Destroy(gameObject);
    }

    public IEnumerator EnemyRotate(float value)
    {

        float currentRotation = 0.0f;
        while (currentRotation > value)
        {
            float rotationStep = 180 * Time.deltaTime;

            transform.Rotate(Vector3.back, rotationStep);
            currentRotation -= rotationStep;

            yield return null;
        }

        yield break;
    }
}
