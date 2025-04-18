using UnityEngine;

public class EnemyFlip : MonoBehaviour
{
    [SerializeField] private bool _lookLeft;
    [SerializeField] private float _rotationAngle;
    [SerializeField] private Transform _transform;
    [SerializeField] private PlayerSearch _playerSearch;

    private void Update()
    {
        if(_playerSearch.MoveDirection.x >= 0.1 && _lookLeft)
        {
            HorizontalFlip();
        }
        if (_playerSearch.MoveDirection.x < 0 && !_lookLeft)
        {
            HorizontalFlip();
        }
    }

    private void HorizontalFlip()
    {
        if (_lookLeft)
        {
            _transform.rotation = Quaternion.Euler(0, _rotationAngle, 0);
            _lookLeft = !_lookLeft;
        }
        else
        {
            _transform.rotation = Quaternion.Euler(0, 0, 0);
            _lookLeft = !_lookLeft;
        }
    }
}
