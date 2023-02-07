using Helpers;
using Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public WeaponItem CurrentWeapon;

    public WeaponItem MeleeWeapon;
    public WeaponItem RangedWeapon;

    public ShellsContainer ShellContainer;

    public Vector3 CurrentTargetPoint;

    #region Attack
    public void SetDamage(Vector3 hitPoint)
    {
        CurrentTargetPoint = hitPoint;

        switch (CurrentWeapon.weaponType)
        {
            case WeaponType.Melee:
                MeleeAttack();
                break;
            case WeaponType.Ranged:
                RangedAttack();
                break;
            case WeaponType.Throwing:
                ThrowingAttack();
                break;
        }
    }

    void MeleeAttack()
    {
        //TO-DO: заглушка
        AttackType curAttack = CurrentWeapon.AttackTypes[0];

        Vector3 position = transform.transform.position;
        GameObject.FindGameObjectsWithTag(GetEnemies())
            .Where(
                e => Geometry.PointInCircle(e.transform.position, position, curAttack.Distance)
                  && Geometry.ValidAngle(position, e.transform.position, CurrentTargetPoint, curAttack.Angle)
                  //TO-DO: && враг
            ).ToList()
            .ForEach(
                e => e.GetComponent<ActionManager>().TakeDamage()
            );
    }

    void RangedAttack()
    {
        Vector3 position = transform.transform.position;
        Vector3 endpoint = Geometry.GetEndpoing(position, CurrentTargetPoint, CurrentWeapon.AttackTypes[0].Distance);
        ShellContainer.CreateShell(CurrentWeapon.ShellSprite, position, endpoint);
    }

    void ThrowingAttack()
    { 
    }

    #endregion

    #region Weapon
    public void SetWeapon(WeaponItem meleeWeapon, WeaponItem rangedWeapon)
    {
        CurrentWeapon = MeleeWeapon = meleeWeapon;
        RangedWeapon = rangedWeapon;
    }
    public void ToggleCurrentWeapon()
    {
        switch (CurrentWeapon.weaponType)
        {
            case WeaponType.Melee:
                CurrentWeapon = RangedWeapon;
                break;
            default:
                CurrentWeapon = MeleeWeapon;
                break;
        }
    }

    public void ToggleCurrentWeapon(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.Melee:
                CurrentWeapon = MeleeWeapon;
                break;
            default:
                CurrentWeapon = RangedWeapon;
                break;
        }
    }

    public string CurrentWeaponType()
    {
        return CurrentWeapon.weaponType.ToString();
    }

    #endregion

    public void ChangeCurrent()
    { 

    }

    //TO-DO: заглушка
    string GetEnemies()
    {
        return "Enemy";
    }
}
