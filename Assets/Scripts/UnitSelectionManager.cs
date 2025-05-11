using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager Instance { get; private set; }
    bool isSelectedGroupIsPlayers = true;

    //Check if player hold the CTRL key
    bool isControlInput = false;

    [SerializeField] 
    public List<GameObject> allPlayersUnits { get; private set; } = new List<GameObject>();
    public List<GameObject> allEnemysUnits { get; private set; } = new List<GameObject>();
    [SerializeField]
    public List<GameObject> selectedUnits { get; private set; } = new List<GameObject>();

    [SerializeField] LayerMask unitLayerMask;
    [SerializeField] LayerMask groundLayerMask;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        RTSInputController.OnChooseInput += ChooseHandler;
        RTSInputController.OnControlInput += ControlHandler;
        RTSInputController.OnControlInputCanceled += ControlCanceledHandler;
    }

    private void ControlCanceledHandler()
    {
        isControlInput = false;
    }

    private void ControlHandler()
    {
        isControlInput = true;
    }

    private void ChooseHandler()
    {
        RaycastHit hit;
        Debug.Log(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, unitLayerMask));
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit,Mathf.Infinity,unitLayerMask))
        {
            if (hit.transform.parent == null) return;
            if (hit.transform.parent.TryGetComponent<Unit>(out Unit unit))
            {
                if (unit.isPlayerUnit)
                {
                    
                     DeselectAll();
                    
                    selectedUnits.Add(hit.transform.parent.gameObject);


                    EnableMovement(hit.transform.parent.gameObject, true);

                }
            }
        } else
        {
            DeselectAll();
            
        }
    }

    private void EnableMovement(GameObject unitGO, bool mover)
    {
        if (unitGO.TryGetComponent<UnitMovement>(out UnitMovement unitMovement))
        {
            unitMovement.enabled = mover;
        }
        if (unitGO.TryGetComponent<Unit>(out Unit unit))
        {
            unit.IndicateSelection(mover);
        }
    }

    public void DeselectAll()
    {
        if (isControlInput) return; //CTRL disables the deselection
        foreach (GameObject unit in selectedUnits)
        {
            EnableMovement(unit, false);
        }
        selectedUnits.Clear();
    }

    public void AddUnit(GameObject unit)
    {
        if(unit.TryGetComponent<Unit>(out Unit unitComponent))
        {
            if(unitComponent.isPlayerUnit)
            {
                allPlayersUnits.Add(unit);
            }
            else
            {
                allEnemysUnits.Add(unit);
            }
        }
        
    }
    public void RemoveUnit(GameObject unit)
    {
        if (unit.TryGetComponent<Unit>(out Unit unitComponent))
        {
            if (unitComponent.isPlayerUnit)
            {
                allPlayersUnits.Remove(unit);
            }
            else
            {
                allEnemysUnits.Remove(unit);
            }
        }

    }

    internal void DragSelect(GameObject unit)
    {
        if(!selectedUnits.Contains(unit))
        {
            selectedUnits.Add(unit);
            EnableMovement(unit, true);
        }
    }
}
