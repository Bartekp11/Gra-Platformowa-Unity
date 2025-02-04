﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
   
 
public ParticleSystem sword;
    public Animator animator;

    public LayerMask enemyLayers;
    public Transform attackPoint;
    public float attackRange = 0.5f;  
    public int attackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    //public Animator camAnim;



    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
             if(Input.GetKeyDown(KeyCode.Z))
        {
            SoundManagement.PlaySound("attack");
            //camAnim.SetTrigger("shake");
            sword.Play();
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
        }
       
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);


        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

    }
    void OnDrawGizmosSelected()
{
    if(attackPoint == null)
        return;
    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
}
}


