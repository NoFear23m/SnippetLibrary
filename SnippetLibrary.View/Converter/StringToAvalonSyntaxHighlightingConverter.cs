
using ICSharpCode.AvalonEdit;
using System;
using System.Linq;
using System.Windows;

namespace SnippetLibrary.View.Helper
{
    public class AvalonTextEditorBindingHelper
    {


        public static string GetSnippetText(TextEditor obj)
        {
            return (string)obj.GetValue(SnippetTextProperty);
        }

        public static void SetSnippetText(DependencyObject obj, string value)
        {
            obj.SetValue(SnippetTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for SnippetText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SnippetTextProperty =
            DependencyProperty.RegisterAttached("SnippetText", typeof(string), typeof(AvalonTextEditorBindingHelper), new PropertyMetadata(string.Empty, SnippetTextProperty_Changed));

        private static void SnippetTextProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextEditor editor = (TextEditor)d;

            editor.TextChanged -= SnippetText_Changed; //Handler entfernen, damit jetzt immer keiner vorhanden ist.
            editor.TextChanged += SnippetText_Changed; //Handler hinzufügen

            editor.Document.Text = e.NewValue.ToString();
        }


        private static void SnippetText_Changed(object sender, EventArgs e)
        {
            TextEditor editor = (TextEditor)sender;
            editor.SetValue(SnippetTextProperty, editor.Document.Text);
        }
    }
}