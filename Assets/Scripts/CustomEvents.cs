using System;
using UnityEngine;
using UnityEngine.Events;


//Separamos os nossos eventos personalizados em um �nico arquivo para facilitar a manuten��o


[Serializable]
public class FloatEvent : UnityEvent<float> { }

[Serializable]
public class IntEvent : UnityEvent<int> { }

[Serializable]
public class BoolEvent : UnityEvent<bool> { }

[Serializable]
public class StringEvent : UnityEvent<string> { }

[Serializable]
public class GameObjectEvent : UnityEvent<GameObject> { }

[Serializable]
public class InputEvent : UnityEvent<float, float> { }

[Serializable]
public class NPCVendedorEvent : UnityEvent<SO_Inventory> { }

[Serializable]
public class EquipEvent : UnityEvent<SO_Weapons> { }

[Serializable]
public class GroupEvent : UnityEvent<Player_Group> { }

