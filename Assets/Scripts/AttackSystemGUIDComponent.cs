using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystemGUIDComponent : MonoBehaviour
{
    public Guid ID;
    private void Awake()
    {
        ID = Guid.NewGuid();
    }
}