// Util.cs
//
// Owen Malchow
// 
// Util class: contains utility methods
using System;

public class Util
{
    // Utility method for capitalizing a string
    static public string Capitalize(string inputString)
    {
        if (inputString == null)
            return null;

        if (inputString.Length < 2)
            return inputString.ToUpper();
        else
            return inputString.Substring(0, 1).ToUpper() + inputString.Substring(1).ToLower();
    }
}
