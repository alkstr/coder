using System.Linq;

namespace Coder.Core.CaesarCipher;

public static class Encoder
{
    public static string Encode(string text, int shift) =>
        string.Concat(
            Utilities.CipherNormalize(text)
                .Select(c => Utilities.LowerEnglishAlphabet.Contains(c) ? Utilities.Shift(c, shift, Utilities.LowerEnglishAlphabet) : c)
                .Select(c => Utilities.LowerRussianAlphabet.Contains(c) ? Utilities.Shift(c, shift, Utilities.LowerRussianAlphabet) : c)
                .Chunk(5)
                .Select(a => a.Append(' '))
                .SelectMany(a => a)
        );
}