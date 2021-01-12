﻿using System.Collections.Generic;
using UnityEngine;

public class CleanerManager : MonoBehaviour
{
    [Header("Instantiate Settings")]
    [SerializeField] private GameObject cleanerPrefab;
    [SerializeField] private Vector3 spawnPos;
    
    private readonly Dictionary<GameObject, CleanerInfo> hiredCleaners = new Dictionary<GameObject, CleanerInfo>();

    public void AddCleaner(CleanerInfo cleanerInfo)
    {
        GameObject newCleaner = Instantiate(cleanerPrefab, spawnPos, Quaternion.identity);
        newCleaner.GetComponent<CleaningAI>().Setup(cleanerInfo);
        
        hiredCleaners.Add(newCleaner, cleanerInfo);
    }
    
    public void RemoveCleaner(GameObject cleaner)
    {
        hiredCleaners.Remove(cleaner);
        Destroy(cleaner);
    }
    
    public CleanerInfo GetCleanerByObject(GameObject gameObj)
    {
        foreach (var kvp in hiredCleaners)
        {
            if (kvp.Key == gameObj) return kvp.Value;
        }
        
        print($"No cleaner found with {gameObj}!");
        
        return new CleanerInfo();
    }
    
    public int GetTotalExpenses()
    {
        int total = 0;
        
        foreach (var cleaner in hiredCleaners.Values)
        {
            total += cleaner.Wage;
        }
        
        return total;
    }
}