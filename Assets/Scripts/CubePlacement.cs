using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubePlacement : MonoBehaviour
{
    [SerializeField] private Transform _grid;
    [SerializeField] private Transform _rightHandTransform;
    [SerializeField] private GameObject _prefabRedCube;
    [SerializeField] private GameObject _prefabBlueCube;
    [SerializeField] private Transform _cubesParent;

    private Plane _gridPlane;
    private float _enter;
    private Ray _ray;

    [SerializeField] private MoveCubes SpawnAction;
    private GameObject _currentCubePrefab;
    private GameObject _currentSelectedCube;
    private Transform _currentPlacementPoint;
    private float _lastRotation = 0f;

    private InputActionReference _rotateAction;

    private Transform temp;

    private void Awake()
    {
        SpawnAction = new MoveCubes();
    }

    void Start()
    {
        _currentCubePrefab = _prefabRedCube;
        _gridPlane = new Plane(Vector3.back, _grid.transform.position);
    }

    private void OnEnable()
    {
        SpawnAction.SpawnCube.spawncube.Enable();
        SpawnAction.SpawnCube.spawncube.performed += SpawnACube;
        SpawnAction.SpawnCube.rotate.Enable();
        SpawnAction.SpawnCube.rotate.performed += RotateCube;
        SpawnAction.SpawnCube.change.Enable();
        SpawnAction.SpawnCube.change.performed += ChangeCube;
    }

    private void OnDisable()
    {
        SpawnAction.SpawnCube.spawncube.Disable();
        SpawnAction.SpawnCube.rotate.Disable();
        SpawnAction.SpawnCube.change.Disable();
    }

    void Update()
    {
        temp = GetNearestPoint();
        if(temp != _currentPlacementPoint)
        {
            _currentPlacementPoint = temp;
            Destroy(_currentSelectedCube);
            _currentSelectedCube = Instantiate(_currentCubePrefab, _currentPlacementPoint.position, 
                Quaternion.Euler(Vector3.forward * _lastRotation));
            Color color = _currentSelectedCube.GetComponent<MeshRenderer>().material.color;
            color.a = 0.2f;
            _currentSelectedCube.GetComponent<MeshRenderer>().material.color = color;
        }
    }

    private Transform GetNearestPoint()
    {
        _ray = new Ray(_rightHandTransform.position, _rightHandTransform.forward);
        if (_gridPlane.Raycast(_ray, out _enter))
        {
            Vector3 hitPoint = _ray.GetPoint(_enter);
            return GetNearestPointFromHit(hitPoint);
        }
        Debug.Log("le raycast ne touche pas le plan");
        return null;
    }

    private Transform GetNearestPointFromHit(Vector3 hitPoint)
    {
        Transform nearestPoint = _grid.GetChild(0);
        foreach (Transform t in _grid)
        {
            if ((t.position - hitPoint).magnitude <= (nearestPoint.position - hitPoint).magnitude)
            {
                nearestPoint.GetComponent<Renderer>().material.color = Color.white;
                nearestPoint = t;
                t.GetComponent<Renderer>().material.color = Color.red;
            }
            else t.GetComponent<Renderer>().material.color = Color.white;
        }
        return nearestPoint;
    }

    private void SpawnACube(InputAction.CallbackContext ctx)
    {
        if (_currentSelectedCube?.GetComponent<Cube>().Collide == false)
        {
            Color color = _currentSelectedCube.GetComponent<Renderer>().material.color;
            color.a = 1f;
            _currentSelectedCube.GetComponent<Renderer>().material.color = color;

            _currentSelectedCube.transform.SetParent(_cubesParent);
            _currentSelectedCube = null;
        }
        else
            Debug.Log("espace occup�");
    }

    private void RotateCube(InputAction.CallbackContext ctx)
    {
        _currentSelectedCube?.transform.Rotate(Vector3.forward * 90);
        _lastRotation += 90;
        _lastRotation %= 360;
    }

    private void ChangeCube(InputAction.CallbackContext ctx)
    {
        if (_currentCubePrefab == _prefabBlueCube)
            _currentCubePrefab = _prefabRedCube;
        else
            _currentCubePrefab = _prefabBlueCube;
    }
}
