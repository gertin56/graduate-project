using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public event UnityAction Dead;

    public void Die()
    {
        Dead.Invoke();
    }
}
