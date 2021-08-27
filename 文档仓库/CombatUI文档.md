# CombatUI文档

### CombatUI说明

位于Unity.NJUCS.UI。如需使用，需要 `using Unity.NJUCS.UI` 。

CombatUI文件夹目前有Avater，CombatUI，Compass，CompassLine，StateBar五个文件，分别对应了战斗场景中的各个元素，下面将依次进行介绍。



### Avater

#### 说明

玩家头像控制的脚本，挂载在 `Resources/UIResources/Prefabs/Avater.prefab` 预制体上，可以根据名称从 `Resources/UIResources/Avater/` 文件夹中加载头像图片并显示到UI上。

#### 使用方法

在需要使用头像的UI面板中加入Avatar的预制体，并在初始化时通过 `LoadAvatar(string AvatarName)` 函数进行头像加载，这里的参数是需要加载的头像名称。

例如：

```c#
Avatar PlayerAvatar;
PlayerAvatar.LoadAvatar("Player");
```

#### 成员变量

| 名称   | 变量类型 | 描述                                                         |
| ------ | -------- | ------------------------------------------------------------ |
| avatar | Image    | 头像图片，将图片动态加载到avatar上即可显示在Avatar预制体中。 |

#### 公共函数

##### 1、public void LoadAvatar(string AvatarName)

传入当前玩家名称（头像名称），将动态加载图片到Avatar预制体中。



### CombatUI

#### 说明

控制整个战斗场景的UI的脚本，挂载在 `Resources/UIPrefabs/CombatUI.prefab` 预制体上，目前可以控制玩家头像，玩家血条，玩家法力值条，游戏场景指南针。

#### 使用方法

需要在Start函数中对玩家头像，玩家血条最大生命值，玩家最大法力值等进行初始化；在Update函数中对玩家当前生命值，玩家当前法力值，指南针进行实时更新。

#### 成员变量

| 名称            | 变量类型      | 描述                         |
| --------------- | ------------- | ---------------------------- |
| combatCanvas    | Canvas        | 整个CombatUI的画布           |
| PlayerHealthBar | StateBar      | 玩家血量条                   |
| PlayerManaBar   | StateBar      | 玩家法力值条                 |
| PlayerAvatar    | Avatar        | 玩家头像                     |
| myCompass       | Compass       | 指南针组件                   |
| m_actorManager  | ActorManager  | 管理角色，用于获取需要的玩家 |
| m_cameraManager | CameraManager | 管理相机，用于获取需要的相机 |
| playerHealth    | Health        | 玩家的生命组件               |
| playerMana      | Mana          | 玩家法力组件                 |
| MainCamera      | GameObject    | 主摄像机                     |

#### 私有函数

##### 1、private void InitializePlayerBar(float MaxHealth, float MaxMana)

对玩家的血量条和法力值条进行初始化，传入参数是玩家最大生命值和玩家最大法力值。

##### 2、private void InitializePlayerAvatar(string AvatarName)

对玩家头像进行初始化，传入参数是人物名称（头像名称），函数会调用 `PlayerAvatar` 的 `LoadAvatar(string AvatarName)` 函数，对玩家头像的图片动态加载。



### Compass

#### 说明

指南针控制脚本，挂载在 `Resources/UIResources/Prefabs/Compass.prefab` 预制体上，根据当前主相机的视角来控制UI上指南针的显示。

#### 使用方法

在需要使用指南针的UI界面加入 `Compass` 预制体，并调用 `changeCompass(float zRotation)` 函数来改变指南针的显示，示例如下：

```c#
private Compass myCompass;
private GameObject MainCamera;
if(MainCamera != null && myCompass != null)
{
    myCompass.changeCompass(MainCamera.GetComponent<Transform>().eulerAngles.y);
}
```

#### 成员变量

| 名称             | 变量类型    | 描述                                                       |
| ---------------- | ----------- | ---------------------------------------------------------- |
| LastzRotation    | float       | 记录上一次的指南针方向值                                   |
| CompassLineLeft  | CompassLine | 左侧的指南针条                                             |
| CompassLineRight | CompassLine | 右侧的指南针条（和左侧指南针条一起形成指南针条循环的效果） |
| CompassText      | Text        | 记录当前指南针方向值的文本                                 |

#### 公共函数

##### 1、public void changeCompass(float zRotation)

根据传入参数 `zRotation` ，也即新的当前方向，和原有的 `LastzRotation  ` 进行比较，如不一样，则分别调用 `CompassLineLeft.Scroll(zRotation)` 和 `CompassLineRight.Scroll(zRotation)` 对指南针条进行调整，同时修改 `CompassText` 的内容。



### CompassLine

#### 说明

控制指南针条的脚本，挂载在 `Resources/UIResources/Prefabs/Compass.prefab` 预制体的子物体 `Line` 上，根据偏转角度来调整指南针条的位置，实现指南针的显示。

#### 使用方法

作为 `Compass` 的成员变量存在，所以成员函数只需由 `Compass` 调用，将偏转角度作为参数传入即可。示例如下：

```c#
private CompassLine CompassLineLeft;
private CompassLine CompassLineRight;
CompassLineLeft.Scroll(zRotation);
CompassLineRight.Scroll(zRotation);
```

#### 成员变量

| 名称          | 变量类型 | 描述                                       |
| ------------- | -------- | ------------------------------------------ |
| Line          | Image    | 指南针条图像                               |
| startPosition | Vector3  | 指南针条的起始位置，便于后续对位置进行改变 |

#### 公共函数

##### 1、public void Scroll(float zRotation)

根据传入的偏转角度值，调整指南针条的位置，并且使得指南针条能够在滑动出显示范围后移动到另一端继续滑动。



### StateBar

#### 说明

游戏中任何有状态值的物体的状态条控制脚本，挂载在 `Resources/UIResources/Prefabs/StateBar.prefab` 预制体上，可以根据状态值的最大值设置状态条最大值，并根据状态值现有值实时更新状态条。

#### 使用方法

在 `CombatUI` 界面现有 `PlayerHealthBar` 和 `PlayerManaBar` 两个固定状态条。此外，所有需要状态条的移动物体都需要将状态条预制体加入到 `GameObject` 上，并将StateBar对象添加为 `GameObject` 的一个成员变量，并需要对状态条最大值进行初始化，并在状态值发生变化时调用函数更新状态条并更新位置。

 `CombatUI` 界面的使用示例如下：

```c#
private StateBar PlayerHealthBar;
private StateBar PlayerManaBar;
PlayerHealthBar.Initialize(MaxHealth, MaxHealth);
PlayerManaBar.Initialize(MaxMana, MaxMana);
...
...
PlayerHealthBar.MycurrentValue = playerHealth.CurrentHealth;
PlayerManaBar.MycurrentValue = playerMana.CurrentMana;
```

移动物体状态条使用示例如下：

```c#
private StateBar enemyHealthBar;
...
enemyHealthBar.Initialize(health.MaxHealth, health.MaxHealth);
...
enemyHealthBar.MycurrentValue = health.CurrentHealth;
enemyHealthBar.ChangeAngle("Enemy", MainCamera);
```

#### 成员变量

| 名称           | 变量类型 | 描述                                         |
| -------------- | -------- | -------------------------------------------- |
| content        | Image    | 状态条内颜色显示，为一个 `filled` 类型图片   |
| stateValue     | Text     | 显示状态条当前状态，格式为 `"当前值/最大值"` |
| canvas         | Canvas   | 画布                                         |
| currentFill    | float    | 当前填充值（为一个0到1之间的浮点数）         |
| MyMaxValue     | float    | 最大值（可获取/修改）                        |
| currentValue   | float    | 当前值                                       |
| MycurrentValue | float    | 获取/修改当前值                              |

#### 公共函数

##### 1、public void Initialize(float currentValue, float maxValue)

初始化状态条，传入参数分别为当前值和最大值。

##### 2、public void ChangeAngle(string name, UnityEngine.GameObject camera)

改变状态条角度 ， `name` 为需要改变状态条的物体名称，目前只有为 `"Enemy"` 时允许修改状态条角度（这是为了避免无意中修改了固定状态条的位置）。 `camera` 为需要将状态条面向的物体，通常为主相机。