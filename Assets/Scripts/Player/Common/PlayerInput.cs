using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private string _attack;
    [SerializeField] private string _switchWeapon;
    [SerializeField] private string _horizontalShot;
    [SerializeField] private string _verticalShot;

    public event Action OnAttacked;
    public event Action OnWeaponChanged;

    public Vector2 ShotDir { get; private set; }
    public Vector2 MoveDir { get; private set; }

    private void Update()
    {
        if (Input.GetButtonDown(_attack))
        {
            OnAttacked?.Invoke();
        }

        if (Input.GetButtonDown(_switchWeapon))
        {
            OnWeaponChanged?.Invoke();
        }

        if(Input.GetButtonDown(_horizontalShot))
        {
            ShotDir = new Vector2(Input.GetAxis(_horizontalShot),Input.GetAxis(_verticalShot));
            OnAttacked?.Invoke();
        }    

        if(Input.GetButtonDown(_verticalShot))
        {
            ShotDir = new Vector2(Input.GetAxis(_horizontalShot), Input.GetAxis(_verticalShot));
            OnAttacked?.Invoke();
        }

        MoveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
