using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionbox : MonoBehaviour
{    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out InteractableObject targetObject))
        {
            targetObject.Highlight();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out InteractableObject targetObject))
        {
            targetObject.Lowlight();
        }
    }
}
