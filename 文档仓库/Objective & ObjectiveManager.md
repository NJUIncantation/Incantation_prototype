# Objective & ObjectiveManager

[toc]

## Objective (public abstract class: MonoBehaviour)

### 功能简介

Objective虚基类负责定义一个Objective即游戏目标/任务。

### 成员变量

| 名称                         | 变量类型                  | 描述                                |
| ---------------------------- | ------------------------- | ----------------------------------- |
| Objective                    | string                    | 目标名称                            |
| ObjectiveDescription         | string                    | 目标描述                            |
| IsOptional                   | bool                      | 目标是否为可选                      |
| DelayVisible                 | float                     | 目标延迟时间                        |
| IsCompleted                  | bool                      | 目标是否已完成                      |
| enable                       | bool                      | 目标是否激活                        |
| OnObjectiveCreated(static)   | event Action\<Objective\> | 事件回调：在start中调用             |
| OnObjectiveCompleted(static) | event Action\<Objective\> | 事件回调：在CompleteObjective中调用 |



### 成员函数

1. public bool IsCompleted

   判断任务是否被阻塞(不是可选任务且未被完成)

2. protected virtual void Start()

   start虚函数。子类需要重写start以配置好相应的成员变量，并在函数最开始调用base.Start()。

3. public void UpdateObjective(string descriptionText, string counterText, string notificationText)

   用于更新目标信息。此函数用于向UI等模块传递有关任务的信息，通过广播Events.ObjectiveUpdateEvent来实现。

4.  public void CompleteObjective(string descriptionText, string counterText, string notificationText)

   在目标完成时手动调用。该函数将激活OnObjectiveCompleted回调函数。

参考事件：

1. Events.ObjectiveUpdateEvent。
2. Events.DisplayMessageEvent

### 如何使用

当需要在场景中设置一个新的任务时，应当创建一个继承自Objective虚基类的类，并完成相应的配置，最后将其注册到ObjectiveManager中。

重写start函数，在函数开始时配置description，titile等信息，并调用基类的start。然后，添加必要的事件信息，并实现相应的回调函数。



## ObjectiveManager (public class: MonoBehaviour)

### 功能简介

ObjectiveManager将一个关卡(level或scene)中的所有任务整合起来进行关卡管理。

### 成员变量

| 名称                  | 变量类型          | 描述                             |
| --------------------- | ----------------- | -------------------------------- |
| m_Objectives          | List\<Objective\> | 关卡中已注册的任务集             |
| m_ObjectivesCompleted | bool              | 是否已达成所有目标               |
| TopObjective          | Objective         | 具有最高优先的目标(该逻辑未实现) |
|                       |                   |                                  |

### 成员函数

1. public void EnableObjective(string ObjectiveTitle)

   激活title对应的任务。

2. public void DisableObjective(string ObjectiveTitle)

   暂停title对应的任务。

3. public void ClearAllObjective()

   清除当前的所有objective。该方法也将导致objective对象被销毁。

参考事件：

1. Events.AllObjectivesCompletedEvent。

### 如何使用

场景中的objective脚本在挂载后将自动完成注册。注意，对应特定场景的objective应当挂载到场景中不是DontDestroyOnLoad的对象上。

