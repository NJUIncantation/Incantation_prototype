# EnenmyController

*说明：*

EnemyController继承自VirtualEnemy，定义在Unity.NJUCS.NPC中，是所有enemy的基类，包含了一个敌人应有的基本功能

#### 一、使用方法

对于场景中的所有敌人，都应该挂载EnemyController组件（或继承自该类的其他controller），然后在Inspector面板调整该enemy的基本属性（后面会详细说明），便可以使用该脚本的所有内容。

#### 二、成员变量

| 名称        | 说明                                                         |
| ----------- | ------------------------------------------------------------ |
| enemyStates | 枚举类型私有变量，描述敌人的状态，包含guard、patrol、chase和dead四种状态 |
| sightRadius | 浮点类型公有变量，描述敌人以自己为中心的可视范围半径         |
| lookAtTime  | 浮点类型公有变量，描述敌人到达巡逻点后的观察时间             |
| isGuard     | 布尔类型公有变量，描述敌人是否为Guard型（对应的是会随机巡逻的非Guard型） |
| patrolRange | 浮点类型公有变量，描述非Guard型敌人以初始位置为中心的巡逻范围半径 |
| coolDown    | 浮点类型公有变量，描述敌人的攻击冷却                         |
| attackRange | 浮点类型公有变量，描述敌人普通攻击范围                       |
| skillRange  | 浮点类型公有变量，描述敌人技能攻击范围                       |

