using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;
using UniprixOperations.ProductManager;
using Task = UniprixOperations.TaskManagement.Task;


namespace UniprixOperations.HelperClasses
{
    public class Printer
    {
        public static void PrintProducts(List<ExpiredProduct> products)
        {
            if(products == null || products.Count == 0)
            {
                MessageBox.Show("Aucun produits sélectionné veuillez sélectionner au moins un produit à imprimer.");
                return;
            }

            var dialog = new PrintDialog();

            //Document
            var doc = new FlowDocument();
            doc.PageWidth = 816;
            doc.PageHeight = 1056;
            doc.PagePadding = new Thickness(60);
            doc.ColumnWidth = 1056; // or any large but finite number
            doc.FontSize = 14;
            doc.FontFamily = new FontFamily("Segoe UI");


            //Titre
            doc.Blocks.Add(new Paragraph(new Run("Produits Expirés"))
            {
                FontSize = 20,
                FontWeight = FontWeights.Bold,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(0,0,0,16)
            });

            //Table
            var table = new Table();
            table.CellSpacing = 0;
            table.Columns.Add(new TableColumn { Width = new GridLength(2, GridUnitType.Star) }); // Name
            table.Columns.Add(new TableColumn { Width = new GridLength(2, GridUnitType.Star) }); // Code UPC
            table.Columns.Add(new TableColumn { Width = new GridLength(2, GridUnitType.Star) }); // Date
            table.Columns.Add(new TableColumn { Width = new GridLength(1, GridUnitType.Star) }); // Quantité
            table.Columns.Add(new TableColumn { Width = new GridLength (1, GridUnitType.Star) }); //En tablettes ou non


            var rowGroup = new TableRowGroup();


            //Header
            var headerRow = new TableRow { Background = Brushes.LightGray };
            headerRow.Cells.Add(MakeCell("Nom du produit", true));
            headerRow.Cells.Add(MakeCell("Code UPC", true));
            headerRow.Cells.Add(MakeCell("Date d'expiration", true));
            headerRow.Cells.Add(MakeCell("Quantité", true));
            headerRow.Cells.Add(MakeCell("En tablette", true));
            rowGroup.Rows.Add(headerRow);

            //Données
            foreach(ExpiredProduct product in products)
            {
                var row = new TableRow();
                row.Cells.Add(MakeCell(product.Name, false));
                row.Cells.Add(MakeCell(product.UPCCode.ToString(), false));
                row.Cells.Add(MakeCell(product.ExpiryDate.ToString("dd/MM/yyyy"), false));
                row.Cells.Add(MakeCell(product.Quantity.ToString(), false));
                if (product.ShelfStatus)
                {
                    row.Cells.Add(MakeCell("Oui", false));
                }
                else
                {
                    row.Cells.Add(MakeCell("Non", false));
                }
                rowGroup.Rows.Add(row);
            }

            table.RowGroups.Add(rowGroup);
            doc.Blocks.Add(table);

            //Footer
            doc.Blocks.Add(new Paragraph(new Run($"Nombre de produits total: {products.Count}"))
            {
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 16)
            });

            // Convert FlowDocument → FixedDocument
            FixedDocumentSequence fixedDocSequence = CreateFixedDocumentFromFlow(doc);

            var preview = new PrintPreviewWindow(fixedDocSequence);
            preview.ShowDialog();

            if (dialog.ShowDialog() == true)
            {
                IDocumentPaginatorSource paginator = doc;
                dialog.PrintDocument(paginator.DocumentPaginator, "Produits Expirés");
            }

        }
        public static void PrintTasks(List<Task> tasks)
        {
            if (tasks == null || tasks.Count == 0)
            {
                MessageBox.Show("Aucune tâche sélectionnée, veuillez sélectionner au moins une tâche à imprimer.");
                return;
            }


            // Document
            var doc = new FlowDocument();
            doc.PageWidth = 816;
            doc.PageHeight = 1056;
            doc.PagePadding = new Thickness(60);
            doc.ColumnWidth = 1056;
            doc.FontSize = 14;
            doc.FontFamily = new FontFamily("Segoe UI");

            // Titre
            doc.Blocks.Add(new Paragraph(new Run("Tâches Complétées"))
            {
                FontSize = 20,
                FontWeight = FontWeights.Bold,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 0, 16)
            });

            // Table
            var table = new Table();
            table.CellSpacing = 0;
            table.Columns.Add(new TableColumn { Width = new GridLength(2, GridUnitType.Star) }); // Nom
            table.Columns.Add(new TableColumn { Width = new GridLength(2, GridUnitType.Star) }); // Complété par
            table.Columns.Add(new TableColumn { Width = new GridLength(2, GridUnitType.Star) }); // Date de complétion
            table.Columns.Add(new TableColumn { Width = new GridLength(2, GridUnitType.Star) }); // Heure de complétion

            var rowGroup = new TableRowGroup();

            // Header
            var headerRow = new TableRow { Background = Brushes.LightGray };
            headerRow.Cells.Add(MakeCell("Nom de la tâche", true));
            headerRow.Cells.Add(MakeCell("Complété par", true));
            headerRow.Cells.Add(MakeCell("Date de complétion", true));
            headerRow.Cells.Add(MakeCell("Heure de complétion", true));
            rowGroup.Rows.Add(headerRow);

            // Données
            foreach (Task task in tasks)
            {
                var row = new TableRow();

                row.Cells.Add(MakeCell(task.Name, false));
                row.Cells.Add(MakeCell(task.CompletionUser?.Name ?? "—", false));
                row.Cells.Add(MakeCell(task.CompletionDate.HasValue
                    ? task.CompletionDate.Value.ToString("dd/MM/yyyy")
                    : "—", false));
                row.Cells.Add(MakeCell(task.CompletionDate.HasValue
                    ? task.CompletionDate.Value.ToString("HH:mm")
                    : "—", false));

                rowGroup.Rows.Add(row);
            }

            table.RowGroups.Add(rowGroup);
            doc.Blocks.Add(table);

            // Footer
            doc.Blocks.Add(new Paragraph(new Run($"Nombre de tâches total: {tasks.Count}"))
            {
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 16)
            });

            // Convert FlowDocument → FixedDocument
            // Convert FlowDocument → FixedDocument
            FixedDocumentSequence fixedDocSequence = CreateFixedDocumentFromFlow(doc);

            var preview = new PrintPreviewWindow(fixedDocSequence);
            preview.ShowDialog();

            // Only show print dialog if user didn't cancel
            var dialog = new PrintDialog();
            if (dialog.ShowDialog() == true)
            {
                dialog.PrintDocument(fixedDocSequence.DocumentPaginator, "Tâches Complétées");
            }
        }

        private static TableCell MakeCell(string text, bool isHeader)
        {
            return new TableCell(new Paragraph(new Run(text))
            {
                FontWeight = isHeader ? FontWeights.Bold : FontWeights.Normal
            })
            {
                Padding = new Thickness(6),
                BorderBrush = Brushes.LightGray,
                BorderThickness = new Thickness(0, 0, 0, 1)
            };
        }
        public static FixedDocumentSequence CreateFixedDocumentFromFlow(FlowDocument doc)
        {
            DocumentPaginator paginator = ((IDocumentPaginatorSource)doc).DocumentPaginator;
            paginator.PageSize = new Size(doc.PageWidth, doc.PageHeight);

            MemoryStream ms = new MemoryStream();
            Package package = Package.Open(ms, FileMode.Create, FileAccess.ReadWrite);

            Uri documentUri = new Uri("pack://InMemoryDocument.xps");

            // Remove first in case it was already added from a previous call
            PackageStore.RemovePackage(documentUri);
            PackageStore.AddPackage(documentUri, package);

            XpsDocument xpsDoc = new XpsDocument(package, CompressionOption.Fast, documentUri.AbsoluteUri);

            XpsSerializationManager rsm = new XpsSerializationManager(new XpsPackagingPolicy(xpsDoc), false);
            rsm.SaveAsXaml(paginator);

            return xpsDoc.GetFixedDocumentSequence();
        }


    }
}
