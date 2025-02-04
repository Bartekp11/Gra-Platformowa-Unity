﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movement : MonoBehaviour
{
  
    private AudioSource Audio;
    public ParticleSystem dust;
    bool ismoving = false;
    public float MovementSpeed = 1;
    public float JumpForce = 1;
    public Animator animator;
    private Rigidbody2D _rigidbody;
    private bool facingRight;
    public int maxHealth = 100;
    int PcurrentHealth;
    public HealthBar  Pbar;
    Vector3 respawnPoint;
    private void Start()
    {
        Audio = GetComponent<AudioSource>();
        respawnPoint = transform.position;
        PcurrentHealth = maxHealth;
        Pbar.SetHealth(PcurrentHealth, maxHealth);
        _rigidbody = GetComponent<Rigidbody2D>();
        facingRight = true;
    }
  
    private void Update()
    {
        var movement = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(movement));
        if(movement != 0)
        {
            ismoving = true;
        }
        else
        {
            ismoving = false;
        }
        if(ismoving)
        {
            if(!Audio.isPlaying)
            Audio.Play();
        
        }
        else
        {
            Audio.Stop();
        }
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;
        if((movement < 0 && facingRight) || (movement > 0) && !facingRight)
        {
           dust.Play();
            facingRight = !facingRight;
            transform.Rotate(new Vector3(0, 180, 0));
        }
           
         if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
        {
            SoundManagement.PlaySound("jump");
            dust.Play();
            _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }
       
    
        
        
    }

    public void CreateDust()
    {
        dust.Play();
    }

     public void TakeDamage(int damage)
    {
        PcurrentHealth -= damage;
        
     SoundManagement.PlaySound("hit1");
        Pbar.SetHealth(PcurrentHealth, maxHealth);
        if(PcurrentHealth <= 0)
        {
         respawn();
         PcurrentHealth = maxHealth;
          Pbar.SetHealth(PcurrentHealth, maxHealth);
        }
    }

    public void respawn()
    {
        transform.position = respawnPoint;
    }
    

}
