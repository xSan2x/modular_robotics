using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFollowingController : MonoBehaviour
{
    public Transform _followingTarget;
    UnitMovement _unitMovement;
    // Start is called before the first frame update
    void Start()
    {
        _unitMovement = GetComponent<UnitMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_followingTarget != null)
        {
            _unitMovement.agent.SetDestination(_followingTarget.position);
        }
    }
}
