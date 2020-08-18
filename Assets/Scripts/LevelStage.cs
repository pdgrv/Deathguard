using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStage : MonoBehaviour
{
    [SerializeField] private GameObject _leftDoor;
    [SerializeField] private GameObject _rightDoor;
    [SerializeField] private Spawner _spawner;

    [SerializeField] private Light _dayLight;
    [SerializeField] private float _lightChangeSpeed;
    [SerializeField] private ParticleSystem _startingParticle;

    private void OnEnable()
    {
        _spawner.LevelComplete += OnLevelComplete;
    }

    private void OnDisable()
    {
        _spawner.LevelComplete -= OnLevelComplete;
    }

    private bool _canStartLevel = true;

    private IEnumerator CloseDoor()
    {
        Quaternion targetRotation = Quaternion.Euler(-90, 0, 0);

        while (Quaternion.Angle(_leftDoor.transform.rotation, targetRotation) > 1f)
        {
            _leftDoor.transform.Rotate(Vector3.forward, 100f * Time.deltaTime);
            _rightDoor.transform.Rotate(Vector3.back, 100f * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator OpenDoor()
    {
        Quaternion targetRotation = Quaternion.Euler(-90, 0, -75);

        while (Quaternion.Angle(_leftDoor.transform.rotation, targetRotation) > 1f)
        {
            _leftDoor.transform.Rotate(Vector3.back, 100f * Time.deltaTime);
            _rightDoor.transform.Rotate(Vector3.forward, 100f * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator LightChanger(float targetIntensity)
    {
        while (Mathf.Abs(_dayLight.intensity - targetIntensity) > 0.001f)
        {
            _dayLight.intensity = Mathf.Lerp(_dayLight.intensity, targetIntensity, _lightChangeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void BeginLevel()
    {
        _canStartLevel = false;
        StartCoroutine(CloseDoor());
        StartCoroutine(LightChanger(0f));
        _startingParticle.Pause();

        _spawner.StartLevel();
    }

    private void OnLevelComplete(int level)
    {
        LevelComplete();
    }

    private void LevelComplete()
    {
        StartCoroutine(OpenDoor());
        StartCoroutine(LightChanger(1.2f));
    }

    public void ExitLevel()
    {
        _canStartLevel = true;
        _startingParticle.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_canStartLevel && other.TryGetComponent(out Player player))
        {
            BeginLevel();
        }
    }
}
