## InputManager 使用文档

InputManger.cs->Class CrossPlatformInputManager允许将输入的逻辑功能与实体操作分离，从而实现跨平台输入控制。

要实现针对特定平台的输入控制，应当继承虚基类VirtualInput，重写相应的虚函数并向CrossPlatformInputManager注册。

自定义的VirtualAxis和VirtualButton需要向CrossPlatformInputManager注册。

### CrossPlatformInputManager

1. public static void RegisterVirtualButton(VirtualButton button)
2. public static void RegisterVirtualAxis(VirtualAxis axis)
3. public static void UnRegisterVirtualButton(string name)
4. public static void UnRegisterVirtualAxis(string name)
5. public static bool AxisExists(string name)
6. public static bool ButtonExists(string name)
7. public static float GetAxis(string name)
8. public static float GetAxisRaw(string name)
9. public static bool GetButton(string name)
10. public abstract void GetKey(string name);
11. public abstract void GetKeyDown(string name);
12. public abstract void GetKeyUp(string name);
13. public static bool GetButtonDown(string name)
14. public static bool GetButtonUp(string name)
15. public void SetVirtualMousePositionX(float f)
16. public void SetVirtualMousePositionY(float f)
17. public void SetVirtualMousePositionZ(float f)
18. public static Vector3 mousePosition

关于GetKey:

![image-20210803184708309](C:\Users\Yongp\AppData\Roaming\Typora\typora-user-images\image-20210803184708309.png)

### VirtualAxis

### VirtualButton