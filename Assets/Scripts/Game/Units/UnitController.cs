using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(UnitParametrs))]
[RequireComponent(typeof(DamagableEntity))]
[RequireComponent(typeof(AudioSource))]
public class UnitController : MonoBehaviour
{
    [SerializeField] UnitParametrs _parametrs;

    [SerializeField] string _enemyTag;

    [SerializeField] GameObject _movePointsContainer; //container into world of of unit move points
    private List<Vector3> _movePoints; // list of move points positions
    private int _lastMovePointIndex; // last index of used move point posotion
    private Vector3 _currentMovePint; // point, wich unit aggresive now

    private GameObject _currentTourret;

    private AudioSource _sound;
    [SerializeField] Animator _animations;

    private DamagableEntity _damagableEntity;

    private void Awake()
    {
        _damagableEntity = GetComponent<DamagableEntity>();
        _damagableEntity.SetMaxHealth(_parametrs.Health);

        _movePoints = new();
        _sound = GetComponent<AudioSource>();

        //get all posistions of move points game object
        foreach (Transform movePointTransform in _movePointsContainer.transform)
            _movePoints.Add(movePointTransform.position);

    }

    private void Start()
    {
        StartCoroutine(TryAttackEnemy());

        if (_movePoints.Count != 0)
        {
            _currentMovePint = _movePoints[0];
            _lastMovePointIndex = 0;
        }
    }


    private void Update()
    {
        if (transform.position.Equals(_currentMovePint))
            SetNextMovePoint();

        if (_currentTourret != null)
        {
            transform.LookAt(new Vector3(_currentTourret.transform.position.x, transform.position.y, _currentTourret.transform.position.z));
            return;
        }

        //move to current move point if exists
        transform.position = Vector3.MoveTowards(transform.position, _currentMovePint, _parametrs.MoveSpeed);
        transform.LookAt(new Vector3(_currentMovePint.x, transform.position.y, _currentMovePint.z));
        _animations.Play("Move");
    }


    private void SetNextMovePoint()
    {
        int nextPointIdx = _lastMovePointIndex + 1;

        if (_movePoints.Count > nextPointIdx)
        {
            _lastMovePointIndex = nextPointIdx;
            _currentMovePint = _movePoints[nextPointIdx];
        }
    }

    /// <summary>
    /// Trys attacking enemy if possible for endless
    /// </summary>
    /// <returns></returns>
    private IEnumerator TryAttackEnemy()
    {
        while (true)
        {
            if (_currentTourret != null)
                AttackTourret();

            yield return new WaitForSeconds(_parametrs.AttackDelay);
        }
    }

    private void AttackTourret()
    {
        _currentTourret.GetComponent<DamagableEntity>().TakeDamage(_parametrs.Damage, gameObject);

        _sound.clip = _parametrs.SoundAttack;
        _sound.Play();
        _animations.Play("Attack");
    }

    //private void OnDamagableEntityDestroyed(GameObject destroyedObj)
    //{
    //    _currentEnemy = null;
    //    print("enemy dead");
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_enemyTag))
        {
            _currentTourret = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_enemyTag))
        {
            _currentTourret = null;
        }
    }

    public void OnTakeDamage(GameObject source)
    {
        //print($"Entity tag: {tag}, Sorce tag: {source.tag}");
        _sound.clip = _parametrs.SoundGotDamage;
        _sound.Play();
    }

    public void OnShouldDestroy(GameObject source)
    {
        //print($"Entity tag: {tag}, Sorce tag: {source.tag}");

        gameObject.SetActive(false);
        StopCoroutine(TryAttackEnemy());

        _sound.clip = _parametrs.SoundDeath;
        _sound.Play();

        //print(_damagableEntity.LatDamageSource.tag);

        _damagableEntity.LatDamageSource.SendMessage("OnUnitKilled", _parametrs.ScoreCost, SendMessageOptions.DontRequireReceiver);

        //print(_damagableEntity.LatDamageSource.tag);

        //if (_damagableEntity.LatDamageSource.CompareTag(Tags.Player))
        //    _damagableEntity.LatDamageSource.SendMessage("OnUnitKilled", _parametrs.ScoreCost, SendMessageOptions.DontRequireReceiver);
        //else if (_damagableEntity.LatDamageSource.CompareTag(Tags.AllayTourret))
        //    _currentTourret?.SendMessage("OnTargetunitDead", gameObject, SendMessageOptions.DontRequireReceiver);

        Destroy(gameObject, 0);
    }
}
