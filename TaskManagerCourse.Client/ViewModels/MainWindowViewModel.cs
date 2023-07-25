using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaskManagerCourse.Client.Models;
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
        public MainWindowViewModel (AuthToken token, UserModel currentUser)
        {
            Token = token;
            CurrentUser = currentUser;

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
        }

        #region PROPERTIES

        private readonly string _userProjectsBtnName = "My projects";
        private readonly string _userDesksBtnName = "My desks";
        private readonly string _userTasksBtnName = "My tasks";
        private readonly string _userInfoBtnName = "My info";
        private readonly string _logoutBtnName = "Logout";

        private readonly string _manageUserBtnName = "Users";

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

        #endregion

        #region METHODS
        private void OpenMyInfoPage()
        {
            ShowMessage(_userInfoBtnName);
        }

        private void OpenDesksPage()
        {
            ShowMessage(_userDesksBtnName);
        }


        private void OpenProjectsPage()
        {
            ShowMessage(_userProjectsBtnName);
        }

        private void OpenTasksPage()
        {
            ShowMessage(_userTasksBtnName);
        }

        private void Logout()
        {
            ShowMessage(_logoutBtnName);
        }

        private void OpenUserManagement()
        {
            ShowMessage(_manageUserBtnName);
        }

        #endregion

        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
