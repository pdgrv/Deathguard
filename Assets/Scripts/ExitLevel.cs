using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    private LevelStage _levelStage;

    private void Start()
    {
        _levelStage = GetComponentInParent<LevelStage>();            
    }

    private void OnTriggerEnter(Collider other)
    {
        _levelStage.ExitLevel();
    }
}
