
using UnityEngine;

public static class GamePauseState
{
    public static bool PauseMenuOpen { get; private set; }
    public static bool UpgradeMenuOpen { get; private set; }
    public static bool GameOver { get; private set; }

    public static void SetPauseMenu(bool isOpen)
    {
        PauseMenuOpen = isOpen;
        RefreshTimeScale();
    }

    public static void SetUpgradeMenu(bool isOpen)
    {
        UpgradeMenuOpen = isOpen;
        RefreshTimeScale();
    }

    public static void SetGameOver(bool ended)
    {
        GameOver = ended;
        RefreshTimeScale();
    }

    public static void ResetState()
    {
        PauseMenuOpen = false;
        UpgradeMenuOpen = false;
        GameOver = false;
        Time.timeScale = 1f;
    }

    private static void RefreshTimeScale()
    {
        Time.timeScale =
            (PauseMenuOpen || UpgradeMenuOpen || GameOver)
            ? 0f
            : 1f;
    }
}
