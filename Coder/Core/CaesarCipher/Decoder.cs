namespace Coder.Core.CaesarCipher;

public static class Decoder
{
    public static string Decode(string text, int shift) =>
        Encoder.Encode(text, -shift);
}