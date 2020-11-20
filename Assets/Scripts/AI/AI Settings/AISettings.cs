using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "tpopl001/AI/Settings/Settings")]
public class AISettings : ScriptableObject
{
    public float rotSpeed = 10;
    public float moveSpeed = 0.5f;
    public float maxAnimSpeed = 0.5f;
    public float stoppingDist = 2;
    public int attackCount = 3;
    public float timeBetweenAttack = 3;
    public int maxHealth = 100;
}
