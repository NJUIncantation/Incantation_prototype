# ActorManger

位于Unity.NJUCS.Game。

### 使用方法

ActorManager挂载在场景中名为GamePlay的空对象上。

想使用ActorManager，应当在脚本中声明ActorManger的私有变量，并在start()函数中获取其实例：

```C#
private ActorManager m_actorManager;
...
void Start()
{
    ...
    m_actorManager = FindObjectOfType<ActorManager>();
    ...
}
```

比如，获取主角色身上挂载的生命值组可以如此做：

Health health = m_actorManager.FindActorByName("MainCharacter").GetComponent\<Health\>();

### 公有函数

1.  public void CreateActor(string name, GameObject gameObject)

   向actormanager注册一个actor。

2.  public void DeleteActor(string name)

   删去该actor

3. public int AmountOfActors()

   返回场景中actor的数量。

4. public GameObject FindActorByName(string name)

   根据name获得actor的实例。

5. public List<GameObject> FindActorThatHasComponent(string type)

   获取拥有某组件type的所有actor(以list返回)。