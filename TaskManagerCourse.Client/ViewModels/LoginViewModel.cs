using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TaskManagerCourse.Client.Models;
using TaskManagerCourse.Client.Services;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Client.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        UsersRequestService _usersRequestService;

        #region COMMANDS
        public DelegateCommand<object> GetUserFromDBCommand { get; private set; }
        #endregion
        public LoginViewModel()
        {
            _usersRequestService = new UsersRequestService();
            GetUserFromDBCommand = new DelegateCommand<object>(GetUserFromDB);
        }

        public string UserLogin { get; set; }
        public string UserPassword { get; private set; }

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

        private AuthToken _authToken;

        public AuthToken AuthToken
        {
            get => _authToken;
            set
            {
                _authToken = value;
                RaisePropertyChanged(nameof(AuthToken));
            }
        }


        #region METHODS

        private void GetUserFromDB(object parameter)
        {
            var passBox = parameter as PasswordBox;

            UserPassword = passBox.Password;

            AuthToken = _usersRequestService.GetToken(UserLogin, UserPassword);
            if (AuthToken != null)
            {
                CurrentUser = _usersRequestService.GetCurrent(AuthToken);
                if (CurrentUser != null)
                {
                    MessageBox.Show(CurrentUser.FirstName);
                }
            }

        }

       
        #endregion

    }
}
