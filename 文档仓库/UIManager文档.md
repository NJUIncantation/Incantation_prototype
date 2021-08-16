# UIManager文档

### UIManagaer说明

位于Unity.NJUCS.UI。用于管理不同的UI。UIManager为单例设计，通过 `UIManager.Instance` 来获取。

在 `UIPanelType.cs` 中指明了现存UI的枚举类，如下所示（如有增删，会后续修改）：

```c#
namespace Unity.NJUCS.UI
{
    public enum UIPanelType
    {
        CombatUI
    }
}
```

### 使用方法

通过 `UIManager.Instance.PushPanel(UIPanelType type)` 和 `UIManager.Instance.PopPanel()` 函数来对UI进行操作。

例如，当需要放入新一个UI——CombatUI，位于所有现存UI上方时，进行如下操作：

```C#
void Start()
{
    UIManager.Instance.PushPanel(UIPanelType.CombatUI);
}
```

即可放置一个CombatUI。

进行如下操作：

```
void Start()
{
    UIManager.Instance.PopPanel();
}
```

即可将最上层UI移除。

### 公共函数

##### 1、 `public void PushPanel(UIPanelType type)` 

暂停当前最上层UI，放置type所指向的UI并启动该UI。

##### 2、 `public void PopPanel()` 

完全关闭最上层UI并移除，将次一层UI恢复（解除暂停）。