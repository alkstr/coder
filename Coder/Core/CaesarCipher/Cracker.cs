using System;
using System.Collections.Generic;
using System.Linq;

namespace Coder.Core.CaesarCipher;

public static class Cracker
{
    public static string Crack(string text)
    {
        var (shift, min) = (0, double.MaxValue);
        for (var i = 0; i < Utilities.LowerRussianAlphabet.Length; i++)
        {
            var newMin = CalculateShiftValueProbability(text, i);
            if (newMin < min)
            {
                (shift, min) = (i, newMin);
            }
        }

        return Decoder.Decode(text, shift);
    }
    
    private static double CalculateShiftValueProbability(string text, int shift)
    {
        // using least squares method
        var charToFrequency = CalculateCharFrequencies(text);

        var leastSquaresSum = charToFrequency
            .Select(kvp => 
                Math.Pow(
                    Utilities.LowerRussianCharToFrequencyInText[kvp.Key] -
                    charToFrequency[Utilities.Shift(kvp.Key, shift, Utilities.LowerRussianAlphabet)], 
                    2))
            .Sum();

        return leastSquaresSum;
    }
    
    private static Dictionary<char, double> CalculateCharFrequencies(string text)
    {
        var length = text.Length;
        var charToCount = Utilities.LowerRussianAlphabet
            .ToDictionary(ch => ch, ch => text.Count(x => x == ch))
            .ToDictionary(kvp => kvp.Key, kvp => (double)kvp.Value / length);
        
        return charToCount;
    }
}