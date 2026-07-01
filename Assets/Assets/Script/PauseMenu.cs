using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject quitPanel;

    [Header("XR Simulator")]
    public GameObject xrSimulator;

    private bool isPaused = false;

    private void Start()
    {
        quitPanel.SetActive(false);

        Time.timeScale = 1f;
        AudioListener.pause = false;

        // Mouse selalu terlihat
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!isPaused)
            {
                OpenPauseMenu();
            }
            else
            {
                ClosePauseMenu();
            }
        }
    }

    public void OpenPauseMenu()
    {
        isPaused = true;

        quitPanel.SetActive(true);

        // Pause seluruh game
        Time.timeScale = 0f;
        AudioListener.pause = true;

        // Matikan XR Simulator
        if (xrSimulator != null)
            xrSimulator.SetActive(false);
    }

    public void ClosePauseMenu()
    {
        isPaused = false;

        quitPanel.SetActive(false);

        // Lanjutkan game
        Time.timeScale = 1f;
        AudioListener.pause = false;

        // Aktifkan kembali XR Simulator
        if (xrSimulator != null)
            xrSimulator.SetActive(true);
    }

    public void QuitGame()
    {
        // Kembalikan keadaan normal sebelum keluar
        Time.timeScale = 1f;
        AudioListener.pause = false;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}