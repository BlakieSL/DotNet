using System;

namespace Project3.Tests;

public static class MyAssert
{
    public static void Throws<T>(Action func) where T : Exception
    {
        var exceptionThrown = false;
        try
        {
            func.Invoke();
        }
        catch (T)
        {
            exceptionThrown = true;
        }

        if (!exceptionThrown)
        {
            throw new Xunit.Sdk.XunitException(
                $"An exception of type {typeof(T)} was expected, but not thrown");
        }
    }
}