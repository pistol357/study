using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ObservablHealthProperty
{
    private Action _onChangeValue;
    private int _minValue = 0;
    private int _value;
    private int _maxValue;

    public int Value
    {
        get => _value;

        set
        {
            _value = value;

            if (_value < _minValue)
            {
                _value = _minValue;

            }
            if (_value > _maxValue) _value = _maxValue;
            RefreshUI();
        }
    }

    public int MaxValue
    {
        get => _maxValue;

        set
        {
            _maxValue = value;

            if (_maxValue < _value) _maxValue = value;
        }
    }

    public ObservablHealthProperty(int value)
    {
        _value = value;
        _maxValue = value;
    }

    public ObservablHealthProperty(int minValue, int value)
    {
        _minValue = minValue;
        _value = value;
        _maxValue = value;
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
