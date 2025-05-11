using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameLib;


public class Module : ScriptableObject
{
    public ModuleType _type;
    public string _name;
    public int _armor;
    public int _weight;
    public int _titanCost;
    public int _batteryCost;
    public int _plasmCost;
    public int _microchipCost;
    //public GameObject _model;
    //public float offsetY;
}
