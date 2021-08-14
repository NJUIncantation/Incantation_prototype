# Mana法力值系统

### 功能概述

Class Mana定义了角色释放技能的法力值消耗。

通过将Mana挂载到对应角色上来使其工作。想要访问Mana脚本函数，请使用gameObject.GetComponent\<Mana\>()。

### 成员变量

| 名称               | 变量类型                       | 描述               |
| ------------------ | ------------------------------ | ------------------ |
| MaxMana            | float                          | 最大法力值         |
| ManaRestoringSpeed | float                          | 每秒法力回复       |
| OnManaGained       | UnityAction<float, GameObject> | 当获取法力值时调用 |
| OnManaSpent        | UnityAction<float, GameObject> | 当消耗法力值时调用 |
| CurrentMana        | float                          | 当前法力值         |

### 成员函数

1. public bool IsFull()

   法力值是否为满。

2. public void ClearMana()

   清除所有法力值。该函数调用不触发OnManaSpent。

3. public void ResetMana(float maxMana)

   重设法力值。该函数调用不触发OnManaSpent或OnManaGain。

4. public bool HaveEnoughMana(float manacost)

   对于manacost，是否有足够的法力值来释放。

5. public void GainMana(float mana, GameObject source)

   从source处获取mana法力。

6. public bool SpendMana(float mana, GameObject spendon)

   spendon处花费mana法力。

   

   5、6函数将会触发对应的action。想要自定义action，请在对象脚本中使用`mana.OnManaSpent += CustomFunction(...)`来添加监听器。

   

   