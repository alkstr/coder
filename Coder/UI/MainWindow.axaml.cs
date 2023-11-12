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
    
    public void EncodeButtonClicked(object source, RoutedEventArgs args)
    {
        if (string.IsNullOrEmpty(PlainTextTextBox.Text))
        {
            ShowErrorFlyout(source, "Исходный текст не может быть пустым");
            return;
        }
        if (!int.TryParse(ShiftTextBox.Text, out var shift))
        {
            ShowErrorFlyout(source, "Сдвиг должен быть целым числом");
            return;
        }
        EncodedTextTextBox.Text = Core.CaesarCipher.Encoder.Encode(PlainTextTextBox.Text, shift);
    }

    public void DecodeButtonClicked(object source, RoutedEventArgs args)
    {
        if (string.IsNullOrEmpty(EncodedTextTextBox.Text))
        {
            ShowErrorFlyout(source, "Зашифрованный текст не может быть пустым");
            return;
        }
        if (!int.TryParse(ShiftTextBox.Text, out var shift))
        {
            ShowErrorFlyout(source, "Сдвиг должен быть целым числом");
            return;
        }
        PlainTextTextBox.Text = Core.CaesarCipher.Decoder.Decode(EncodedTextTextBox.Text, shift);
    }

    public void CrackButtonClicked(object source, RoutedEventArgs args)
    {
        if (string.IsNullOrEmpty(EncodedTextTextBox.Text))
        {
            ShowErrorFlyout(source, "Зашифрованный текст не может быть пустым");
            return;
        }
        PlainTextTextBox.Text = Core.CaesarCipher.Cracker.Crack(EncodedTextTextBox.Text);
    }
}