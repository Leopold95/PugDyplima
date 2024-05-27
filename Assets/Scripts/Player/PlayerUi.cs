using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerUi : MonoBehaviour
{
    [SerializeField] GameObject _mainIntefase;
    [SerializeField] GameObject _esacapeMenu;

    [SerializeField] PostProcessVolume _asset;

    public bool IsEscapeOpen { get; private set; }

    private void Awake()
    {
        CloseEsacapeMenu();
    }

    public void OpenEsacapeMenu()
    {
        _mainIntefase.SetActive(false);
        _esacapeMenu.SetActive(true);
        IsEscapeOpen = true;
        _asset.enabled = true;
        //print("opened");
    }

    public void CloseEsacapeMenu()
    {
        _mainIntefase.SetActive(true);
        _esacapeMenu.SetActive(false);
        IsEscapeOpen = false;
        _asset.enabled = false;
        //print("closed");
    }
}
