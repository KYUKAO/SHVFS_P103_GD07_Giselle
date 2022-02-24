using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableComponent : MonoBehaviour
{
    public AttackSystemGUIDComponent AttackGUIDComponent;
    public Guid GUID ;

    private void Start()
    {
        GUID = AttackGUIDComponent.ID;
    }
}
