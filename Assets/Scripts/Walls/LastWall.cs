using UnityEngine;
using UnityEngine.Events;

public class LastWall : MonoBehaviour
{
    public event UnityAction Reached;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            Reached?.Invoke();
        }
    }
}
