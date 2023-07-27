﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TaskManagerCourse.Client.Models;
using TaskManagerCourse.Client.Views;
using TaskManagerCourse.Client.Views.Pages;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Client.ViewModels
{
    public class MainWindowViewModel: BindableBase
    {
        #region COMMANDS

        public DelegateCommand OpenMyInfoPageCommand;
        public DelegateCommand OpenDesksPageCommand;
        public DelegateCommand OpenProjectsPageCommand;
        public DelegateCommand OpenTasksPageCommand;
        public DelegateCommand LogoutCommand;

        public DelegateCommand OpenUserManagementCommand;


        #endregion 
        public MainWindowViewModel (AuthToken token, UserModel currentUser, Window currentWindow= null)
        {
            Token = token;
            CurrentUser = currentUser;
            _currentWindow = currentWindow;
            

            OpenMyInfoPageCommand = new DelegateCommand (OpenMyInfoPage);
            NavButtons.Add(_userInfoBtnName,OpenMyInfoPageCommand);

            OpenDesksPageCommand = new DelegateCommand (OpenDesksPage);
            NavButtons.Add(_userDesksBtnName,OpenDesksPageCommand);

            OpenProjectsPageCommand = new DelegateCommand (OpenProjectsPage);
            NavButtons.Add(_userProjectsBtnName,OpenProjectsPageCommand);

            OpenTasksPageCommand = new DelegateCommand (OpenTasksPage);
            NavButtons.Add(_userTasksBtnName,OpenTasksPageCommand);

            if (CurrentUser.Status == UserStatus.Admin)
            {
                OpenUserManagementCommand = new DelegateCommand(OpenUserManagement);
                NavButtons.Add(_manageUserBtnName,OpenUserManagementCommand);
            }

            LogoutCommand = new DelegateCommand (Logout);
            NavButtons.Add(_logoutBtnName,LogoutCommand);

            OpenMyInfoPage();
        }

        #region PROPERTIES

        private readonly string _userProjectsBtnName = "My projects";
        private readonly string _userDesksBtnName = "My desks";
        private readonly string _userTasksBtnName = "My tasks";
        private readonly string _userInfoBtnName = "My info";
        private readonly string _logoutBtnName = "Logout";

        private readonly string _manageUserBtnName = "Users";

        private Window _currentWindow;

        private AuthToken _token;

        public AuthToken Token
        {
            get => _token;
            private set
            {
                _token = value;
                RaisePropertyChanged(nameof(Token));
            }
        }

        private UserModel _currentUser;

        public UserModel CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                RaisePropertyChanged(nameof(CurrentUser));
            }
        }

        private Dictionary<string, DelegateCommand> _navButtons = new Dictionary<string, DelegateCommand>();

        public Dictionary<string, DelegateCommand> NavButtons
        {
            get => _navButtons; 
            set 
            { 
                _navButtons = value; 
                RaisePropertyChanged(nameof(NavButtons));
            }
        }

        private string _selectedPageName;
        public string SelectedPageName
        {
            get => _selectedPageName;
            set
            {
                _selectedPageName = value;
                RaisePropertyChanged(nameof(SelectedPageName));
            }
        }

        private Page _selectedPage;
        public Page SelectedPage
        {
            get => _selectedPage;
            set
            {
                _selectedPage = value;
                RaisePropertyChanged(nameof(SelectedPage));
            }
        }

        #endregion

        #region METHODS
        private void OpenMyInfoPage()
        {
            var page = new UserInfoPage();
            OpenPage(page, _userInfoBtnName, this);
            
        }

        private void OpenDesksPage()
        {
            SelectedPageName = _userDesksBtnName;
            ShowMessage(_userDesksBtnName);
        }


        private void OpenProjectsPage()
        {
            SelectedPageName = _userProjectsBtnName;
            ShowMessage(_userProjectsBtnName);
        }

        private void OpenTasksPage()
        {
            var page = new UserTasksPage();
            OpenPage(page, _userTasksBtnName, new UserTasksPageViewModel(Token));
        }

        private void Logout()
        {
            var question = MessageBox.Show("Are you sure?", "Logout", MessageBoxButton.YesNo);
            if (question == MessageBoxResult.Yes && _currentWindow != null)
            {
                Login login = new Login();
                login.Show();
                _currentWindow.Close();
            }
            ShowMessage(_logoutBtnName);
        }

        private void OpenUserManagement()
        {
            SelectedPageName = _manageUserBtnName;
            ShowMessage(_manageUserBtnName);
        }

        #endregion

        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void OpenPage(Page page, string pageName, BindableBase viewModel)
        {
            SelectedPageName = pageName;
            SelectedPage = page;
            SelectedPage.DataContext = viewModel;
        }
    }
}
