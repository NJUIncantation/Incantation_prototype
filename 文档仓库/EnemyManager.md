# EnemyManager

位于 Unity.NJUCS.NPC

#### 一、使用方法

```c#
public class EnemyManager : Singleton<EnemyManager>
```

该类继承自Singleton单例类，挂载在EnemyManager上，可直接通过 `EnemyManager.Instance`来使用



#### 二、公有函数

```C#
public void AddEnemy (VirtualEnemy enmey);
public void RemoveEnemy (VirtualEnemy enmey);
```

分别向敌人容器中增加/移除enemy

```C#
 public List<VirtualEnemy> GetEnemies;
```

返回一个包含所有enemy的list