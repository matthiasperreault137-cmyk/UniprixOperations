using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UniprixOperations
{
    /// <summary>
    /// Logique d'interaction pour PrintPreviewWindow.xaml
    /// </summary>
    public partial class PrintPreviewWindow : Window
    {
        public PrintPreviewWindow(FixedDocumentSequence document)
        {
            InitializeComponent();
            DocViewer.Document = document;
            Loaded += (s, e) => RemoveToolbar();
        }

        private void RemoveToolbar()
        {
            var findToolbar = DocViewer.Template.FindName("PART_FindToolBarHost", DocViewer) as FrameworkElement;
            if (findToolbar != null)
                findToolbar.Visibility = Visibility.Collapsed;

            var mainToolbar = DocViewer.Template.FindName("PART_ToolBarHost", DocViewer) as FrameworkElement;
            if (mainToolbar != null)
                mainToolbar.Visibility = Visibility.Collapsed;
        }

    }
}
