using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : SingletonManager<CoroutineManager>
{
    private Dictionary<string, Coroutine> _coroutines = new();
    private WaitForSeconds _ms = new (0.01f);

    public void RunCoroutine(IEnumerator coroutine)
    {
        string corName = nameof(coroutine);

        if (_coroutines.ContainsKey(corName))
        {
            StopCoroutine(corName);
        }
        
        _coroutines[corName] = StartCoroutine(coroutine);
    }

    public void StopCoroutine(string corName)
    {
        if (_coroutines.ContainsKey(corName))
        {
            StopCoroutine(_coroutines[corName]);
            _coroutines.Remove(corName);
        }
    }

    public void StopAllCustomCoroutines()
    {
        foreach (var cor in _coroutines.Values)
        {
            StopCoroutine(cor);
        }
        _coroutines.Clear();
    }
}
