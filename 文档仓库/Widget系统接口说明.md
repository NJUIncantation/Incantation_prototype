# Widget系统接口说明

### public abstract class FlyingObjectBase : MonoBehaviour

**描述：**

属于Unity.NJUCS.Widget

所有飞行物/子弹的抽象基类，所有武器需要发射飞行物都需要声明一个FlyingObjectBase

**成员变量：**

| 名称                    | 描述                           |
| ----------------------- | ------------------------------ |
| Owner                   | 记录飞行物的发射者             |
| InitialPosition         | 飞行物本身的初始坐标           |
| InitialDirection        | 飞行物本身的初始朝向           |
| InheritedMuzzleVelocity | 枪口速度（武器本身运动速度）   |
| OnShoot                 | 绑定动作响应（在子类中重定义） |

**成员函数：**

```C#
public void Shoot(WeaponController controller)
```

从controller控制的武器中发射该飞行物




### public class FlyingObjectStandard : FlyingObjectBase

**描述：**

属于Unity.NJUCS.Widget

所有飞行物/子弹的prefab都应挂载此组件，其父类是class FlyingObjectBase

在Update中实现飞行物空中的移动与消亡

**成员变量：**

| 名称                    | 描述                                  |
| ----------------------- | ------------------------------------- |
| Radius                  | 弹丸碰撞检测的半径                    |
| Root                    | 飞行物根的transform（用于碰撞检测）   |
| Tip                     | 飞行物顶点的transform（用于碰撞检测） |
| MaxLifeTime             | 子弹生存时间                          |
| HittableLayers          | 碰撞检测的LayerMask                   |
| Speed                   | 子弹速度                              |
| GravityDownAcceleration | 子弹受到的重力加速度                  |
| InheritWeaponVelocity   | 是否计算武器本身的速度                |
| Damage                  | 飞行物伤害                            |
| AreaOfEffect            | 伤害范围                              |

**成员函数：**

飞行物的成员函数都是*私有函数*，外部调用只能使用父类FlyingObjectBase 的`Shoot()`函数，以下说明在每个函数内实现的功能

```C#
void OnEnable()
```

实例化飞行物时被调用，在实际武器代码中`FlyingObjectBase newFlyingObject = Instantiate(FlyingObjectPrefab,WeaponMuzzle.position,WeaponMuzzle.rotation);`实例化一个新的飞行物

```C#
new void OnShoot()
```

重写父类FlyingObjectBase的public UnityAction `OnShoot`

```C#
void Update()
```

飞行物生成后在空中的飞行与消亡，并利用Raycast检测是否发生碰撞，如果发生碰撞调用`OnHit()`

```C#
bool IsHitValid(RaycastHit hit)
```

判断碰撞是否合法

```C#
void OnHit()
```

发生碰撞，产生damageArea，飞行物被销毁



### public class WeaponController : MonoBehaviour

**描述：**

属于Unity.NJUCS.Widget

武器的控制模块，所有武器都应挂载此组件，实现武器攻击/技能释放等功能

**成员变量：**

| 名称                     | 描述                                     |
| ------------------------ | ---------------------------------------- |
| WeaponName               | 武器名字                                 |
| WeaponMuzzle             | 存放枪口的位置                           |
| ShootType                | 武器攻击类型                             |
| BulletSpreadAngle        | 随机扩散的最大角度(0表示没有扩散)        |
| BulletsPerShot           | 一次发射弹丸的数量                       |
| SingleFlyingObjectPrefab | 单发子弹预制体                           |
| SpreadFlyingObjectPrefab | 霰弹子弹预制体                           |
| Owner                    | 记录谁拥有这个武器                       |
| SourcePrefab             | 用来判断实例的武器是否来自于同一个Prefab |
| MuzzleWorldVelocity      | 武器（人物）移动速度                     |

**成员函数：**

```c#
public bool TryShoot()
```

（单发，无扩散）用来判断当前武器能否发射（还没做）并实例化子弹并发射，无视BulletsPerShot的值，返回值表示是否能发射

```c#
public bool TryShootSpread()
```

（多发，散射）用来判断当前武器能否发射并实例化BulletsPerShot颗子弹并发射，返回值表示是否能发射

```c#
public Vector3 GetShotDirectionWithinSpread(Transform shootTransform)
```

随机散射函数，输入发射的transform，返回一个BulletSpreadAngle角度内的发射方向



### public class PlayerWeaponManager : MonoBehaviour

**描述：**

属于Unity.NJUCS.Game

管理player的武器系统，player应挂载该组件实现武器攻击逻辑，在`void update()`中实现根据input来攻击

**成员变量：**

| 名称               | 描述                          |
| ------------------ | ----------------------------- |
| StartingWeapons    | 开始时装备的武器              |
| WeaponParentSocket | 所有武器被添加到该父transform |
| ActiveWeaponIndex  | 当前正在使用的武器编号        |

**成员函数：**

```C#
public bool AddWeapon(WeaponController weaponPrefab)
```

添加一个武器，参数为添加的武器

```C#
public void ActivedWeaponShootSingle()
```

当前使用的武器单发开火

```C#
public void ActivedWeaponShootSpread()
```

当前使用的武器散发开火

```C#
public WeaponController HasWeapon(WeaponController weaponPrefab)
```

判断一个weaponPrefab是否已被添加，如果没有则返回该weaponPrefab，如果已经被添加则返回null，避免重复添加武器