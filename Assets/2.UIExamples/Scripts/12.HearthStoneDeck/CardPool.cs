using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class CardPool
{
    private List<Transform> _activeList;
    private List<Transform> _inactiveList;
    private Transform _parent;
    private Transform _prefab;
    private Type[] _component;

    public CardPool(Transform parent,Transform prefab,params Type[] component)
    {
        _parent = parent;
        _prefab = prefab;
        _component = component;
        _activeList = new List<Transform>();
        _inactiveList = new List<Transform>();
    }

    public Transform Spawn(Transform parent)
    {
        Transform card;
        if (_inactiveList.Count > 0)
        {
            card = _inactiveList[0];
            _inactiveList.Remove(card);
        }
        else
        {
            card = SpawnNew();
        }

        card.SetParent(parent);
        _activeList.Add(card);

        return card;
    }

    public void Despwan(Transform card)
    {
        if (_activeList.Contains(card))
        {
            card.transform.SetParent(_parent);
            _activeList.Remove(card);
            _inactiveList.Add(card);
        }
    }

    private Transform SpawnNew()
    {
        GameObject go = Object.Instantiate(_prefab, _parent).gameObject;
        foreach (Type type in _component)
        {
            go.AddComponent(type);
        }
        return go.transform;
    }
}
