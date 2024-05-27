using System.Collections;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] GameObject _spawnPoint;

    [SerializeField] GameObject _tierOne;
    [SerializeField] GameObject _tierTwo;
    [SerializeField] GameObject _tierThree;
    [SerializeField] GameObject _tierFour;

    [SerializeField] bool _isSpawningActive;

    [SerializeField] float _spawnDelay;
    [SerializeField] GameStage _gameStage;


    [SerializeField] float _timeToSecondStage = 5;
    [SerializeField] float _timeToThirdStage = 10;
    [SerializeField] float _timeToFourthStage = 15;

    private void Start()
    {
        StartCoroutine(BeginSpawninng());

        StartCoroutine(BeginSecondStage());
        StartCoroutine(BeginThirdStage());
        StartCoroutine(BeginFourhStage());
    }

    IEnumerator BeginSpawninng()
    {
        while (true)
        {
            if (_isSpawningActive == false)
            {
                yield return null;
                continue;
            }

            switch (_gameStage)
            {
                case GameStage.First:
                    var unit = Instantiate(_tierOne);
                    unit.transform.position = _spawnPoint.transform.position;
                    break;
                case GameStage.Second:
                    var unit2 = Instantiate(_tierTwo);
                    unit2.transform.position = _spawnPoint.transform.position;
                    break;
                case GameStage.Third:
                    var unit3 = Instantiate(_tierThree);
                    unit3.transform.position = _spawnPoint.transform.position;
                    break;
                case GameStage.Fourth:
                    var unit4 = Instantiate(_tierFour);
                    unit4.transform.position = _spawnPoint.transform.position;
                    break;
            }

            yield return new WaitForSeconds(_spawnDelay);
        }
    }


    private IEnumerator BeginSecondStage()
    {
        yield return new WaitForSeconds(_timeToSecondStage);
        _gameStage = GameStage.Second;
    }

    private IEnumerator BeginThirdStage()
    {
        yield return new WaitForSeconds(_timeToThirdStage);
        _gameStage = GameStage.Third;
    }

    private IEnumerator BeginFourhStage()
    {
        yield return new WaitForSeconds(_timeToFourthStage);
        _gameStage = GameStage.Fourth;
    }

    private void OnDestroy()
    {
        StopCoroutine(BeginSpawninng());
    }
}
