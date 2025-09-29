using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    [SerializeField] public PathManager _pathManager;
    [SerializeField] Animator _animator;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _rotateSpeed;

    List<Waypoint> _thePath;
    Waypoint _target;

    bool _isWalking;

    void Start()
    {
        _isWalking = false;
        _animator.SetBool("isWalking", _isWalking);

        _thePath = _pathManager.GetPath();
        if(_thePath != null && _thePath.Count > 0)
            _target = _thePath[0];
    }

    void RotateTowardsTarget()
    {
        float stepSize = _rotateSpeed * Time.deltaTime;

        Vector3 targetDir = _target._position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepSize, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
    void MoveForward()
    {
        float stepSize = Time.deltaTime * _moveSpeed;
        float distanceToTarget = Vector3.Distance(transform.position, _target._position);
       
        if (distanceToTarget < stepSize) return;
        
        Vector3 moveDir = Vector3.forward;
        transform.Translate(moveDir * stepSize);
    }

    void Update()
    {
        if(Input.anyKeyDown)
        {
            _isWalking = !_isWalking;
            _animator.SetBool("isWalking", _isWalking );
        }
        if (_isWalking)
        {
            RotateTowardsTarget();
            MoveForward();
        }
    }

    void OnTriggerEnter(Collider other) => _target = _pathManager.GetNextTarget();
}