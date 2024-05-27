using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(PlayerMoney))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerUi))]
public class Player : MonoBehaviour
{
    private PlayerInputHandler _playerInputHandler;
    private PlayerMoney _money;
    private PlayerUi _ui;

    [SerializeField] PlayerVisuals _playerVisualsController;

    private void Awake()
    {
        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _money = GetComponent<PlayerMoney>();
        _ui = GetComponent<PlayerUi>();

        _playerInputHandler.OnEscapePressed.AddListener(EscapePressed);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        _playerInputHandler.UpdatePlayerMovement();
        _playerVisualsController.UpdatePlayerVisuals(_playerInputHandler.PlayerState);
        _playerInputHandler.UpdatePlayerInput();
    }

    private void EscapePressed()
    {
        if (_ui.IsEscapeOpen)
        {
            _ui.CloseEsacapeMenu();
            return;
        }
            

        _ui.OpenEsacapeMenu();
    }

    public void OnUnitKilled(int score)
    {
        _money.Add(score);
        //print(score);
    }
}
