using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [SerializeField] private int _keysRequired;
    public int _keys;

    public void GetKeys()
    {
        _keys++;
        if (_keys == _keysRequired) Destroy(gameObject);
    }
}
