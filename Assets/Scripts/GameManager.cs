using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private CharacterSheet characterSheet;

    public void StartGame()
    {
        characterSheet.Reset();
    }
}
