﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeForce : MonoBehaviour
{
    [SerializeField] private int max_health = 100;

    public CustomEvents.GameObjectEvent on_death_event;
    public CustomEvents.IntEvent on_damage_event;
    public CustomEvents.IntEvent on_health_changed_event;

    private int current_health = 100;


    void Awake()
    {
        CreateEvents();
    }


    private void CreateEvents()
    {
        if (on_death_event == null)
            on_death_event = new CustomEvents.GameObjectEvent();//create event

        if (on_damage_event == null)
            on_damage_event = new CustomEvents.IntEvent();
    }


    public void Damage(int _damage)
    {
        if (current_health <= 0)
            return;

        current_health -= _damage;//damage health
        current_health = Mathf.Clamp(current_health, 0, int.MaxValue);
        on_health_changed_event.Invoke(current_health);

        if (current_health > 0)
        {
            on_damage_event.Invoke(_damage);//trigger damage event if survived
        }
        else
        {
            on_death_event.Invoke(gameObject);//trigger death event
        }
    }


    public void Heal(int _heal_amount)
    {
        current_health += _heal_amount;
        current_health = Mathf.Clamp(current_health, 0, max_health);//clamp to max value
    }


    public void ResetHealth()
    {
        current_health = max_health;
    }


    public void SetMaxHealth(int _max_health, bool _update_current_health = true)
    {
        max_health = _max_health;

        if (!_update_current_health)
            return;

        ResetHealth();//update current health if specified
    }

}
