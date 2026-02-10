using UnityEngine;

public class LevelComplete : MenuBase
{
    public override GameMenus MenuType()
    {
        return GameMenus.LevelCompleteMenu;
    }

    public void ButtonNextLevel()
    {
        LevelMgr.Instance.NextLevel();
        SceneMgr.Instance.LoadScene(GameScenes.Gameplay, GameMenus.InGameUI);
    }
}
