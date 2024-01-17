using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInput : MonoBehaviour
{
    public void GetTouch()
    {
        SpaceShipShoot.instance.Shoot();
    }
}
