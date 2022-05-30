using DeathIsOnlyTheBeginning.Controlls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] List<DoorController> doors;

    public List<DoorController> Doors { get { return doors; } }
}
