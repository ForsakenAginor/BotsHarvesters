using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Harvester : MonoBehaviour
{
    [SerializeField] ResourceStorage _storage;

    private Transform _target;
    private Animator _animator;
    private bool _isReturning;
    private float _speed = 10;


    public bool IsBusy { get; private set; }

    public void SetTarget(Transform target)
    {
        _target = target;
        IsBusy = true;
    }

    private void Awake()
    {
        IsBusy = false;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsBusy)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, Time.deltaTime * _speed);
            transform.LookAt(_target.position);
        }

         _animator.SetBool("IsWalking", IsBusy);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == _target.GetComponent<Collider>() && _target.TryGetComponent<Resource>(out Resource resouce))
        {
            _target = _storage.transform;
            resouce.transform.SetParent(transform);
            _isReturning = true;
        }
        else if (other == _storage.GetComponent<Collider>() && _isReturning == true)
        {
            IsBusy = false;
            _isReturning = false;
        }
    }
}
