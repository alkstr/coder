using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;

namespace Coder.UI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    // ///////////////////////
    // Generic
    // ///////////////////////
    
    private static void ShowErrorFlyout(object source, string errorText)
    {
        var control = (Control)source;
        FlyoutBase.ShowAttachedFlyout(control);
        var flyout = (Flyout)FlyoutBase.GetAttachedFlyout(control)!;
        flyout.Content = errorText;
    }
    
    // ///////////////////////
    // Caesar Cipher
    // ///////////////////////
    
    public void EncodeClick(object source, RoutedEventArgs args)
    {
        if (string.IsNullOrEmpty(PlainTB.Text))
        {
            ShowErrorFlyout(source, "Исходный текст не может быть пустым");
            return;
        }
        if (!int.TryParse(ShiftTB.Text, out var shift))
        {
            ShowErrorFlyout(source, "Сдвиг должен быть целым числом");
            return;
        }
        
        PlainTB.Text = string.Concat(PlainTB.Text.Select(char.ToLower));
        EncodedTB.Text = Core.CaesarCipher.Encoder.Encode(PlainTB.Text, shift);
    }

    public void DecodeClick(object source, RoutedEventArgs args)
    {
        if (string.IsNullOrEmpty(EncodedTB.Text))
        {
            ShowErrorFlyout(source, "Зашифрованный текст не может быть пустым");
            return;
        }
        if (!int.TryParse(ShiftTB.Text, out var shift))
        {
            ShowErrorFlyout(source, "Сдвиг должен быть целым числом");
            return;
        }
        
        EncodedTB.Text = string.Concat(EncodedTB.Text.Select(char.ToLower));
        PlainTB.Text = Core.CaesarCipher.Decoder.Decode(EncodedTB.Text, shift);
    }

    public void CrackClick(object source, RoutedEventArgs args)
    {
        if (string.IsNullOrEmpty(EncodedTB.Text))
        {
            ShowErrorFlyout(source, "Зашифрованный текст не может быть пустым");
            return;
        }

        EncodedTB.Text = string.Concat(EncodedTB.Text.Select(char.ToLower));
        PlainTB.Text = Core.CaesarCipher.Cracker.Crack(EncodedTB.Text);
    }
}