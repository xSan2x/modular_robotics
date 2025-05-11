using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    UnitSelectionManager unitSelectionManager;
    [SerializeField] GameObject unitSelectionIndicator;
    [SerializeField] Color _friendlyColor;
    [SerializeField] Color _neutralColor;
    [SerializeField] Color _enemyColor;

    public bool isPlayerUnit = true; // true for player, false for enemy
    // Start is called before the first frame update
    void Start()
    {
        unitSelectionManager = UnitSelectionManager.Instance;
        unitSelectionManager.AddUnit(gameObject);
    }

    private void OnDestroy()
    {
        unitSelectionManager.RemoveUnit(gameObject);
    }

    public void IndicateSelection(bool active)
    {
        StopAllCoroutines();
        unitSelectionIndicator.GetComponent<Renderer>().material.color = isPlayerUnit ? _friendlyColor : _enemyColor;
        unitSelectionIndicator.SetActive(active);
    }

    public void IndicateFollowing()
    {
        StartCoroutine(IndicateFollow());
    }

    private IEnumerator IndicateFollow()
    {
        
                unitSelectionIndicator.GetComponent<Renderer>().material.color = isPlayerUnit ? _neutralColor : _enemyColor;
                unitSelectionIndicator.SetActive(true);
                yield return new WaitForSeconds(0.3f);
                unitSelectionIndicator.SetActive(false);
                yield return new WaitForSeconds(0.3f);
                unitSelectionIndicator.SetActive(true);
                yield return new WaitForSeconds(0.3f);
                unitSelectionIndicator.SetActive(false);
                
            
        
        
    }
}
