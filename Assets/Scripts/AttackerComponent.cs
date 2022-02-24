using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerComponent : MonoBehaviour
{

    //Aint wanna many references ,want this to be self-contained
    //Globally unique identifier 

    public AttackSystemGUIDComponent AttackGUIDComponent;

    public GameObject Attacker;
    public float AttackActiveTime;
    public float AttackPower;
    private float attackActiveTimer;
    private Guid guid;

    void OnEnable()
    {
        Attacker.SetActive(false);
    }
    private void Start()
    {
        guid = AttackGUIDComponent.ID;
    }
    // Update is called once per frame
    void Update()
    {
        if (attackActiveTimer < 0f)
        {
            attackActiveTimer = 0f;
        }
        //FistTurnSmaller
        if (!Input.GetMouseButton(0)) return;
        attackActiveTimer -= Time.deltaTime;
        Attacker.transform.localScale = Vector3.one * attackActiveTimer / AttackActiveTime;
        attackActiveTimer -= Time.deltaTime;
        Attacker.transform.localScale = Vector3.one * attackActiveTimer / AttackActiveTime;

        if (attackActiveTimer > 0f)
        {
            Attacker.SetActive(true);
            return;
        }

        Attacker.SetActive(false);
        attackActiveTimer = AttackActiveTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<AttackableComponent>()) return;
        if (other.GetComponent<AttackableComponent>().GUID == guid) return;
        other.GetComponent<Rigidbody>().AddForce((-transform.forward*AttackPower)+(transform.up * AttackPower), ForceMode.Impulse);
    }
}
