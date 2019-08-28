
using ICSharpCode.AvalonEdit;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace SnippetLibrary.App.View.Converter
{


    public class StringToAvalonSyntaxHighlightingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ICSharpCode.AvalonEdit.Highlighting.IHighlightingDefinition definition = ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.GetDefinition((string)value);

            if (definition != null)
            {
                return definition;
            }
            else { return null; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}