using UnityEngine;
using System.Collections;

public class BasePanel : MonoBehaviour {

    protected UIManager uiManager;
    protected GameFacade facade;

    public UIManager UIMgr
    {
        set
        {
            uiManager = value;
        }
    }

    public GameFacade Facade
    {
        set { facade = value; }
    }

    protected void PlayClickSound()
    {
        facade.PlayNormalSound(AudioManager.Sound_ButtonClick);
    }

    /// <summary>
    /// 界面被显示出来
    /// </summary>
    public virtual void OnEnter()
    {

    }

    /// <summary>
    /// 界面暂停
    /// </summary>
    public virtual void OnPause()
    {

    }

    /// <summary>
    /// 界面继续
    /// </summary>
    public virtual void OnResume()
    {

    }

    /// <summary>
    /// 界面不显示,退出这个界面，界面被关系
    /// </summary>
    public virtual void OnExit()
    {

    }
}
