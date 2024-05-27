using Cinemachine.Utility;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(DamagableEntity))]
public class TourretController : MonoBehaviour
{
    [SerializeField] string _aimTag; //who tourrent will be attacking
    [SerializeField] TourretParametrs _params;

    private DamagableEntity _damagableEntity;
    private AudioSource _soundPlayer;
    private GameObject _currentUnit;

    private void Awake()
    {
        _damagableEntity =  GetComponent<DamagableEntity>();
        _soundPlayer = GetComponent<AudioSource>();

        _damagableEntity.SetMaxHealth(_params.Health);
    }

    private void OnUnitDetected(GameObject unit)
    {
        if (_currentUnit == unit)
            return;

        _currentUnit = unit;

    
        
        StartCoroutine(TryAttack(unit));
    }

    public void OnTakeDamage(GameObject source)
    {
        //print($"tourret took damage from: {source.tag}");
    }

    public void OnShouldDestroy(GameObject source) 
    {
        print($"tourret dead of: {source.tag}");

        _soundPlayer.clip = _params.DeathSound;
        _soundPlayer.Play();

        Destroy(gameObject);
    }

    public void OnUnitKilled(int cost)
    {
        //print("tourret killed unit");
    }

    //Message from unit
    private void OnTargetunitDead(GameObject deadUnit)
    {
        _currentUnit = null;
        StopCoroutine(TryAttack(deadUnit));
        //print("tourret killed unit");
    }

    private IEnumerator TryAttack(GameObject target)
    {
        while (true)
        {
            if(_currentUnit != null || gameObject != null)
            {
                target.GetComponent<DamagableEntity>().TakeDamage(_params.Damage, gameObject);
                _soundPlayer.clip = _params.AttackhSound;
                _soundPlayer.Play();
            }

            yield return new WaitForSeconds(_params.AttackDelaySeconds);
        }
    }
}
