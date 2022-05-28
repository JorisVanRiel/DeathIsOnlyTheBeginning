using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Sheet")]
public class CharacterSheet : ScriptableObject
{
    private int experiencePoints;
    private int attack;
    private int defence;

    public int ExperiencePoints { get { return experiencePoints; } }
    public int Attack { get { return attack; } }
    public int Defence { get { return defence; } }

    public void AddExperiencePoints(int amount)
    {
        experiencePoints += amount;
    }

    public void Reset()
    {
        experiencePoints = 0;
    }
}
