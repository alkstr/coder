using System;
using System.Collections.Generic;
using System.Linq;

namespace Coder.Core.CaesarCipher;

public static class Cracker
{
    // finds most possible shift
    public static (int, string) Crack(string text)
    {
        // range from 0 to alphabet length
        var shiftWithMinSum = Enumerable.Range(0, Utilities.LowerRussianAlphabet.Length)
            // map to (shift, sum)
            .Select(shift => (shift, CalculateLeastSquaresSum(text, shift)))
            // find pair with min sum
            .MinBy(kvp => kvp.Item2)
            // get shift
            .Item1;

        return (shiftWithMinSum, Decoder.Decode(text, shiftWithMinSum));
    }
    
    private static double CalculateLeastSquaresSum(string text, int shift)
    {
        // for every char get their relative frequencies in text
        var charToRelFrequency = CalculateCharRelFrequencies(text);

        // ∑(GlobalFrequency_i - InputShiftFrequency_i)²
        var leastSquaresSum = charToRelFrequency
            .Select(kvp => 
                Math.Pow(
                    // relative frequency of kvp.Key in russian text
                    Utilities.LowerRussianCharToFrequencyInText[kvp.Key] -
                    // relative frequency of shifted kvp.Key in input 
                    charToRelFrequency[Utilities.Shift(kvp.Key, shift, Utilities.LowerRussianAlphabet)], 
                    2))
            .Sum();

        return leastSquaresSum;
    }
    
    private static Dictionary<char, double> CalculateCharRelFrequencies(string text)
    {
        // length of text with only russian chars
        var length = text.Count(c => Utilities.LowerRussianAlphabet.Contains(c));

        var charToRelFrequency = Utilities.LowerRussianAlphabet
            .ToDictionary(c => c, c => (double)text.Count(x => x == c) / length);
        
        return charToRelFrequency;
    }
}