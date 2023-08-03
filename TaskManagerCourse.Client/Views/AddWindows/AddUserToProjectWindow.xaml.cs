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
using TaskManagerCourse.Client.ViewModels;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Client.Views.AddWindows
{
    /// <summary>
    /// Логика взаимодействия для AddUserToProjectWindow.xaml
    /// </summary>
    public partial class AddUserToProjectWindow : Window
    {
        public AddUserToProjectWindow()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = (ProjectsPageViewModel)DataContext;
            foreach (UserModel user in e.RemovedItems)
            {
                viewModel.SelectedUsersForProject.Remove(user);
            }

            foreach (UserModel user in e.AddedItems)
            {
                viewModel.SelectedUsersForProject.Add(user);
            }
        }
    }
}
