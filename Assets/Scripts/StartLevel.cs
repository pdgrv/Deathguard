using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    [SerializeField] private GameObject LeftDoor;
    [SerializeField] private GameObject RightDoor;

    private IEnumerator CloseDoor()
    {
        Quaternion targetRotation = Quaternion.Euler(-90, 0, 0);

        while (Quaternion.Angle(LeftDoor.transform.rotation, targetRotation) > 1f)
        {
            LeftDoor.transform.Rotate(Vector3.forward, 100f * Time.deltaTime);
            RightDoor.transform.Rotate(Vector3.back, 100f * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            StartCoroutine(CloseDoor());
        }
    }
}
