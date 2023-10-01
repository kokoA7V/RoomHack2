using UnityEngine;

public class TutorialBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<TutorialEnemy>(out TutorialEnemy enemy))
        {
            enemy.hp--;
            Destroy(gameObject);
        }
        else if (collision.gameObject.TryGetComponent<TutorialEnemy2>(out TutorialEnemy2 enemy2))
        {
            enemy2.hp--;
            Destroy(gameObject);
        }
    }
}
