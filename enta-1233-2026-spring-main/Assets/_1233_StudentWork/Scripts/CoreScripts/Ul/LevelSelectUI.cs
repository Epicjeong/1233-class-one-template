using UnityEngine;

public class LevelSelectUI : MenuBase
{
    [SerializeField] private Transform _contentParent;
    [SerializeField] private LevelSelectEntryUI _entryPrefab;

    public override GameMenus MenuType()
    {
        return GameMenus.LevelSelectMenu;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        BuildEntries();
    }

    private void ButtonBack()
    {
        UIMgr.Instance.HideMenu(GameMenus.LevelSelectMenu);
    }

    private void BuildEntries()
    {
        var levels = LevelMgr.Instance.LevelSceneNames;
        if (levels.Length == 0 ) return;

        for (var i = 0; i < levels.Length; i++)
        {
            var entry = Instantiate(_entryPrefab, _contentParent);
            entry.Setup(levels[i], i);
        }
    }
}
