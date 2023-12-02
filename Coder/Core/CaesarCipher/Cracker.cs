using System;
using System.Collections.Generic;
using System.Linq;

namespace Coder.Core.CaesarCipher;

public static class Cracker
{
    // finds most possible shift
    public static string Crack(string text)
    {
        var (shift, prob) = (0, double.MaxValue);
        for (var s = 0; s < Utilities.LowerRussianAlphabet.Length; s++)
        {
            var p = CalculateShiftValueProbability(text, s);
            if (p < prob)
            {
                (shift, prob) = (s, p);
            }
        }

        return Decoder.Decode(text, shift);
    }
    
    // using least squares method
    private static double CalculateShiftValueProbability(string text, int shift)
    {
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