using UnityEngine;

public class WinBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameMgr.Instance.NextLevel();
        }
    }
}
