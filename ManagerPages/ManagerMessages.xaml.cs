using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using UniprixOperations.HelperClasses;
using UniprixOperations.MessageManagement;
using UniprixOperations.PageData.ManagerPageData;
using UniprixOperations.UserManagement;
using UniprixOperations.XmlDataManager;

namespace UniprixOperations.ManagerPages
{
    /// <summary>
    /// Logique d'interaction pour ManagerMessages.xaml
    /// </summary>
    public partial class ManagerMessages : Page, IPageDef
    {
        private ManagerMessagesData data;
        public ManagerMessages(ManagerMessagesData data)
        {
            InitializeComponent();
            this.data = data;
            Refresh();
        }
        //Side Bar Navigation Functions
        public void OpenMainMenu(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenManagerMainMenu();
        }
        public void OpenTasks(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenManagerTasks();
        }
        public void OpenExpiredProducts(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenManagerExpireMenu();
        }
        public void OpenTaskHistory(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenTaskHistory();
        }
        public void OpenUserMenu(Object sender, RoutedEventArgs e)
        {
            Navigator.OpenUserMenu();
        }




        public void Refresh()
        {
            Load();
        }


        //Loading

        private Border AddMessage(Message message)
        {
            var border = new Border { Style = (Style)FindResource("MessageBorderStyle") };

            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            var nameBlock = new TextBlock { Text = message.Sender.Name, Style = (Style)FindResource("MessageSenderStyle") };
            Grid.SetRow(nameBlock, 0); Grid.SetColumn(nameBlock, 0);

            var dateBlock = new TextBlock { Text = message.Timestamp.ToString("yyyy-MM-dd HH:mm"), Style = (Style)FindResource("MessageDateStyle") };
            Grid.SetRow(dateBlock, 0); Grid.SetColumn(dateBlock, 1);

            var actionButton = new Button { Content = "🗑️", Style = (Style)FindResource("MessageDeleteButtonStyle"), ToolTip = "Delete" };
            actionButton.Click += (s, e) => DeleteMesssage(message);
            Grid.SetRow(actionButton, 0); Grid.SetColumn(actionButton, 2);

            var msgBlock = new TextBlock { Text = message.Content, Style = (Style)FindResource("MessageContentStyle") };
            Grid.SetRow(msgBlock, 1); Grid.SetColumn(msgBlock, 0); Grid.SetColumnSpan(msgBlock, 3);

            grid.Children.Add(nameBlock);
            grid.Children.Add(dateBlock);
            grid.Children.Add(actionButton);
            grid.Children.Add(msgBlock);
            border.Child = grid;

            return border;
        }

        private void Load()
        {
            MMMessages.Children.Clear();
            for(int i = DataManager.Instance.Messages.MessageList.Count - 1; i>=0; i--)
            {
                Message m = DataManager.Instance.Messages.MessageList[i];
                MMMessages.Children.Add(AddMessage(m));
            }
        }

        //Supprimer les messages
        private void DeleteMessages(Object sender, RoutedEventArgs e)
        {
            DataManager.Instance.Messages.MessageList.Clear();
            Refresh();   
        }

        private void DeleteMesssage(Message message)
        {
            DataManager.Instance.Messages.MessageList.Remove(message);
            Refresh();
        }

        //Envoyer un message

        private void SendMessageClick(Object sender, RoutedEventArgs e)
        {
            SendMessage();
        }
        private void SendMessage()
        {
            string content = MMMessageInput.Text;
            if (!string.IsNullOrEmpty(content))
            {
                User senderUser = UserStore.CurrentUser;
                Message message = new Message(content, senderUser);
                DataManager.Instance.Messages.MessageList.Add(message);
                MMMessageInput.Text = "";
                Refresh();
            }
        }
        private void EnterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Keyboard.Modifiers == ModifierKeys.Shift)
                {
                    MMMessageInput.Text += "\n";
                    MMMessageInput.CaretIndex = MMMessageInput.Text.Length;
                }
                else
                {
                    SendMessage();
                }
            }
        }
    }
}
