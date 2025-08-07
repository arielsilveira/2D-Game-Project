using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager
{
    private PlayerControls playerControls;

    public float Movement => playerControls.Gameplay.Movement.ReadValue<float>();
    public event Action OnJump;
    public event Action OnAttack;
    public event Action OnDash;
    public event Action OnMenuOpenClose;

    public InputManager()
    {
        playerControls = new PlayerControls();
        EnablePlayerGameplayInput();
        EnableUIInput();

        playerControls.Gameplay.Jump.performed += OnJumpPerformed;
        playerControls.Gameplay.Attack.performed += OnAttackPerformed;
        playerControls.UI.OpenCloseMenu.performed += OpenClosePauseMenuPerformed;
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }
    
    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        OnAttack?.Invoke();
    }

    private void OnDashPerformed(InputAction.CallbackContext context)
    {
        OnDash?.Invoke();
    }

    public void DisablePlayerGameplayInput()
    {
      playerControls.Gameplay.Disable();
    }

    public void EnablePlayerGameplayInput()
    {
        playerControls.Gameplay.Enable();
    }

    public void EnableUIInput() => playerControls.UI.Enable();
    private void OpenClosePauseMenuPerformed(InputAction.CallbackContext obj)
    {
        if (SceneManager.GetActiveScene().name != "Gameplay") return;
        OnMenuOpenClose?.Invoke();
    }
   public void DisableUIInput() => playerControls.UI.Disable();
}