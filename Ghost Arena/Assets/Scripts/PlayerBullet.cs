using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour
{
    public delegate void GhostDestroyedEvent();
    public event GhostDestroyedEvent GhostDestroyed;
    public delegate void AccuracyEvent(BulletEventType type);
    public event AccuracyEvent Accuracy;

    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (Globals.CurGameState == GameState.PlayingGame)
        {
            if (collider.gameObject.tag == "Ghost")
            {
                Globals.Score += (Globals.DifficultyLevel + 1) * 5;
                Destroy(collider.gameObject);
                Destroy(gameObject);
                //GhostDestroyed();
                //Accuracy(BulletEventType.Hit);
            }
        }
    }

    void Update ()
    {
        if (Globals.CurGameState == GameState.PlayingGame)
        {
            transform.Translate(Vector2.up * 0.1f);
            //destroy if at arena wall
            if (transform.position.x >= 5 || transform.position.x <= -5 ||
                transform.position.y >= 3.4 || transform.position.y <= -3.4)
            {
                Destroy(gameObject);
                //don't fire destroy event, only fired when player destroys ghost
                //Accuracy(BulletEventType.Miss);
            }
        }
    }
}
