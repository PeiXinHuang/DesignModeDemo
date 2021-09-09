using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// 状态模式 常用于减少大量的 if-else switch 的实现
/// 优点
/// 减少对原有代码的大量修改
/// 减少bug率
/// 符合开闭原则，有利于长期维护
/// 状态管理更加清晰明确，每个状态单独维护自己
/// 多项目开发，可以复用，节约开发成本
/// 缺点
/// 可读性差
/// 增加大量的类



/// 状态模式实现场景切换 StartScene MainMenuScene GameBattleScene 


//实现多个场景
public class StateModeExample : MonoBehaviour {

    SceneStateC m_sceneStateC = new SceneStateC(); //状态控制器（可以单例实现）
    

    public void Awake()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
    public void Start() //开始就加载开始场景
    {
        m_sceneStateC.SetState(new StartState(m_sceneStateC),"StartScene");
    }
    public void Update() //状态循环
    {
        m_sceneStateC.StateUpdate();
    }
}

//开始场景状态器
class StartState : ISceneState
{
    public StartState(SceneStateC c) : base(c)
    {
        this.StateName = "StartState";
    }
    public override void StateBegin()
    {
        Debug.Log("StartState StateBegin"); //一般用于加载资源，初始化，热更新判断
    }
    public override void StateUpdate()
    {
        Debug.Log("StartState StateUpdate"); //书写状态类中需要监听的事件
        m_Controller.SetState(new MainMenuState(m_Controller), "MainMenuScene"); //切换到MainMenuScene场景
    }

}

//主菜单场景状态器
class MainMenuState:ISceneState
{
    public MainMenuState(SceneStateC c) : base(c)
    {
        this.StateName = "MainMenuScene";
    }
    public override void StateBegin()
    {
        Debug.Log("MainMenuScene StateBegin"); //一般用于加载资源，初始化，热更新判断
    }
    public override void StateUpdate()
    {
        Debug.Log("MainMenuScene StateUpdate"); 
        m_Controller.SetState(new GameBattleState(m_Controller), "GameBattleScene"); //切换到BattleScene场景
    }
}

//游戏战斗场景状态器
class GameBattleState : ISceneState
{
    public GameBattleState(SceneStateC c) : base(c)
    {
        this.StateName = "GameBattleScene";
    }
    public override void StateBegin()
    {
        Debug.Log("GameBattleScene StateBegin"); //一般用于加载资源，初始化，热更新判断
    }
    public override void StateUpdate()
    {
        Debug.Log("GameBattleScene StateUpdate");
       
    }
}



//状态父类
 class ISceneState
{
    private string m_StateName = "ISceneState"; //当前状态名称
    public string StateName
    {
        get { return m_StateName; }
        set { m_StateName = value; }
    }
    protected SceneStateC m_Controller = null; //状态控制器
    public ISceneState() { }

    public ISceneState (SceneStateC ssc)
    {
        m_Controller = ssc;
    }

    //状态开始,用于初始化
    public virtual void StateBegin() { }

    //状态结束，用于销毁
    public virtual void StateEnd() { }

    //状态更新
    public virtual void StateUpdate() { } 
}

//状态控制器类
class SceneStateC {
    public ISceneState m_State;
    bool isBegin = false;
    public SceneStateC() { }

    /// <summary>
    /// 设置状态
    /// </summary>
    /// <param name="state">状态</param>
    /// <param name="LoadSceneName">加载的场景名称</param>
    public void SetState(ISceneState state,string LoadSceneName) {
        isBegin = false; //设置状态开始为false
        LoadScene(LoadSceneName); //加载场景
        if (m_State != null) //结束之前的状态
        {
            m_State.StateEnd();
        }
        m_State = state; //设置新的状态
    }

    //加载场景
    private void LoadScene(string loadSceneName)
    {
        if(loadSceneName == null || loadSceneName.Length == 0)
        {
            return;
        }
        SceneManager.LoadScene(loadSceneName);
    }

    //状态更新
    public void StateUpdate()
    {
        if(m_State != null && isBegin == false) //第一次执行，调用StateBegin，之后调用StateUpdate
        {
            m_State.StateBegin();
            isBegin = true;
        }
        if(m_State != null)
        {
            m_State.StateUpdate();
        }
    }
}