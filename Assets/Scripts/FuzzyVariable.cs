using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyVariable {
    public double value;

    public FuzzyVariable(double value)
    {
        this.value = value;
    }


    public void setValue(double value)
    {
        this.value = value;
    }

    public double getValue()
    {
        return value;
    }

    public FuzzyVariable Inverted()
    {
        return new FuzzyVariable(1 - value);
    }

    public static FuzzyVariable operator +(FuzzyVariable v1, FuzzyVariable v2)
    {
        if(v1.getValue() > v2.getValue())
        {
            return new FuzzyVariable(v1.getValue());
        }
        else
        {
            return new FuzzyVariable(v2.getValue());
        }
    }

    public static FuzzyVariable operator *(FuzzyVariable v1, FuzzyVariable v2)
    {
        if(v1.getValue() > v2.getValue())
        {
            return new FuzzyVariable(v2.getValue());
        }
        else
        {
            return new FuzzyVariable(v1.getValue());
        }
    }
}
