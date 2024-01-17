using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SpaceShipShoot : MonoBehaviour
{
    public static SpaceShipShoot instance { get; set; }
    //
    public bool test;
    //
    public float ammo;
    public float shootDelay;
    [SerializeField] private Transform shootArea, shootEffectArea, bulletParent;
    [SerializeField] private GameObject bullet, shootEffect;
    //
    private Text _ammoCounter;
    private bool _canShoot = true;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            throw new Exception("Birden fazla shoot scripi var!");
        }
    }

    private void Start()
    {
        _ammoCounter = GameObject.FindWithTag("AmmoCounter").GetComponent<Text>();
        _ammoCounter.text = ammo.ToString();
    }

    private void Update()
    {
        if (test)
        {
            test = false;
            Shoot();
        }
    }

    public void Shoot()
    {
        if (!_canShoot || !GameManager.instance.isGameStarted)
        {
            return;
        }

        if (ammo <= 0)
        {
            Debug.Log("No ammo");
            _ammoCounter.GetComponent<Animator>().SetTrigger("NoAmmo");
            return;
        }
        var _a = Instantiate(bullet, shootArea);
        _a.transform.SetParent(bulletParent);
        Destroy(_a, 8);
        //
        Instantiate(shootEffect).transform.position = shootEffectArea.transform.position;
        //
        ammo--;
        _ammoCounter.text = ammo.ToString(CultureInfo.CurrentCulture);
        //
        _canShoot = false;
        Invoke(nameof(SetCanShoot), shootDelay);
    }

    public void TakeAmmo(float amount)
    {
        ammo += amount;
        _ammoCounter.text = ammo.ToString(CultureInfo.CurrentCulture);
    }

    private void SetCanShoot() => _canShoot = true;
}
