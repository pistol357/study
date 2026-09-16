using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ObservableProperty<T>
{
    private Action _onChangeValue;
    private T _value;

    public T Value
    {
        get
        {
            return _value;
        }

        set
        {
            _value = value;
            RefreshUI();
        }
    }

    public ObservableProperty(T value)
    {
        Value= value;
    }

    public void AddListener(Action onChangeValue)
    {
        _onChangeValue += onChangeValue;
    }

    public void RemoveListener(Action onChangeValue)
    {
        _onChangeValue -= onChangeValue;
    }

    public void RemoveAllListener()
    {
        _onChangeValue = null;
    }

    private void RefreshUI()
    {
        _onChangeValue?.Invoke();
    }
}
