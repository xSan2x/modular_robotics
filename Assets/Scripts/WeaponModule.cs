using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponModule")]
public class WeaponModule : Module
{
    public int _damagePerSecond;
    public float _range;
    public float _fireRate; // shots per second
    public float _overheatTime;
    public bool _haveSplash;
    public float _splashRadius;
    public float _splashDamageKf;
}
