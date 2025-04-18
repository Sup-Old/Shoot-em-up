using System.Collections.Generic;
using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _rotationAngle;
    [SerializeField] private bool _lookRight;
    [SerializeField] private bool _lookDown;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private List<Sprite> _sprites;

    private void Update()
    {
        if (_input.MoveDir.x <= -0.1 && _lookRight)
        {
            HorizontalFlip();
        }
        if(_input.MoveDir.x > 0 && !_lookRight)
        {
            HorizontalFlip();
        }
        if(_input.MoveDir.y > 0 && _lookDown)
        {
            VerticalFlip();
        }
        if(_input.MoveDir.y < 0 && !_lookDown)
        {
            VerticalFlip();
        }
    }

    private void VerticalFlip()
    {
        if(_lookDown)
        {
            _spriteRenderer.sprite = _sprites[1];
            _lookDown = !_lookDown;
        }
        else
        {
            _spriteRenderer.sprite = _sprites[0];
            _lookDown = !_lookDown;
        }
    }

    private void HorizontalFlip()
    {
        if (_lookRight)
        {
            _transform.rotation = Quaternion.Euler(0, _rotationAngle, 0);
            _lookRight = !_lookRight;
        }
        else
        {
            _transform.rotation = Quaternion.Euler(0, 0, 0);
            _lookRight = !_lookRight;
        }
    }
}
