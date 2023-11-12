using System;
using System.Collections.Generic;
using System.Linq;

namespace Coder.Core.CaesarCipher;

public static class Utilities
{
    // public static readonly char[] LowerEnglishAlphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
    public static readonly char[] LowerRussianAlphabet = "абвгдежзийклмнопрстуфхцчшщъыьэюя".ToCharArray();
    public static readonly Dictionary<char, double> LowerRussianCharToFrequencyInText = new()
    {
        { 'а', 0.062 }, { 'б', 0.014 }, { 'в', 0.038 }, { 'г', 0.013 }, { 'д', 0.025 }, { 'е', 0.072 }, { 'ё', 0.072 },
        { 'ж', 0.007 }, { 'з', 0.016 }, { 'и', 0.062 }, { 'й', 0.010 }, { 'к', 0.028 }, { 'л', 0.035 }, { 'м', 0.026 }, 
        { 'н', 0.053 }, { 'о', 0.090 }, { 'п', 0.023 }, { 'р', 0.040 }, { 'с', 0.045 }, { 'т', 0.053 }, { 'у', 0.021 }, 
        { 'ф', 0.002 }, { 'х', 0.009 }, { 'ц', 0.003 }, { 'ч', 0.012 }, { 'ш', 0.006 }, { 'щ', 0.003 }, { 'ъ', 0.014 },
        { 'ы', 0.016 }, { 'ь', 0.014 }, { 'э', 0.003 }, { 'ю', 0.006 }, { 'я', 0.018 },
    };
    
    public static char Shift(char ch, int value, char[] alphabet)
    {
        if (value < 0)
        {
            value = alphabet.Length + value % alphabet.Length;
        }
        return alphabet[(Array.IndexOf(alphabet, ch) + value) % alphabet.Length];
    }
    
    public static IEnumerable<char> CipherNormalize(IEnumerable<char> str)
    {
        return str
            .Select(char.ToLower)
            .Where(c => LowerRussianAlphabet.Contains(c));
    }
}