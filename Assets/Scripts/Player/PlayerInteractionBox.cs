using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionBox : MonoBehaviour
{
    private InteractableObject _targetObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out InteractableObject targetObject))
        {
            targetObject.Highlight();
            _targetObject = targetObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out InteractableObject targetObject))
        {
            targetObject.Lowlight();
            _targetObject = null;
        }
    }

    public bool Use(Player player)
    {
        if (_targetObject != null)
        {
            _targetObject.Interact(player);
            return true;
        }

        return false;
    }
}
