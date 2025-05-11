using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] LayerMask unitLayerMask;

    UnitFollowingController _unitFollowingController;
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        RTSInputController.OnActInput += ActHandler;
        agent = GetComponent<NavMeshAgent>();
        _unitFollowingController = GetComponent<UnitFollowingController>();
        _unitFollowingController._followingTarget = null;
    }

    private void Update()
    {
        
    }


    private void ActHandler()
    {
        if(this.enabled == false) return;
        RaycastHit hit;
        Debug.Log(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, groundLayerMask));
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, groundLayerMask))
        {
            _unitFollowingController._followingTarget = null;
            agent.SetDestination(hit.point);
        }
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, unitLayerMask))
        {
            Debug.Log("Hit: " + hit.transform.name);
            if (hit.transform != null)
            {
                _unitFollowingController._followingTarget = hit.transform;
                if (hit.transform.parent.TryGetComponent<Unit>(out Unit unit))
                {
                    unit.IndicateFollowing();
                }
            }
        }
    }

}
