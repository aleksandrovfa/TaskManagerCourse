using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerCourse.Client.Models;
using TaskManagerCourse.Client.Services;
using TaskManagerCourse.Client.Views.AddWindows;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Client.ViewModels
{
    public class ProjectsPageViewModel : BindableBase
    {
        private AuthToken _token;
        private UsersRequestService _usersRequestService;
        private ProjectsRequestService _projectsRequestService;
        private CommonViewService _viewService;

        #region COMMANDS
        public DelegateCommand OpenNewProjectCommand { get; private set; }
        public DelegateCommand<object> OpenUpdateProjectCommand { get; private set; }
        public DelegateCommand<object> ShowProjectInfoCommand { get; private set; }
        public DelegateCommand CreateOrUpdateProjectCommand { get; private set; }

        public DelegateCommand DeleteProjectCommand { get; private set; }
        #endregion

        public ProjectsPageViewModel (AuthToken token)
        {
            _viewService = new CommonViewService ();
            _usersRequestService =new  UsersRequestService();
            _projectsRequestService =new ProjectsRequestService (); 

            _token = token;
            OpenNewProjectCommand = new DelegateCommand(OpenNewProject);
            OpenUpdateProjectCommand = new DelegateCommand<object>(OpenUpdateProject);
            ShowProjectInfoCommand = new DelegateCommand<object>(ShowProjectInfo);
            CreateOrUpdateProjectCommand = new DelegateCommand(CreateOrUpdateProject);
            DeleteProjectCommand = new DelegateCommand(DeleteProject);
        }

       
        #region PROPERTIES

        private List<ModelClient<ProjectModel>> _userProjects = new List<ModelClient<ProjectModel>>();

        public List<ModelClient<ProjectModel>> UserProjects
        {
            get => _projectsRequestService.GetAllProjects(_token)
                .Select (project => new ModelClient<ProjectModel> (project))
                .ToList ();
        }

        private ModelClient<ProjectModel> _selectedProject;

        public ModelClient<ProjectModel> SelectedProject
        {
            get => _selectedProject;
            set
            {
                _selectedProject = value;
                RaisePropertyChanged(nameof(SelectedProject));
                if (SelectedProject.Model.AllUsersIds != null && SelectedProject.Model.AllUsersIds.Count>0)
                {
                    ProjectUsers = SelectedProject.Model.AllUsersIds?
                    .Select(userId => _usersRequestService.GetUserById(_token, userId))
                    .ToList();
                }
                else
                {
                    ProjectUsers = new List<UserModel> ();
                }
                
            }
        }

        private List<UserModel> _projectUsers;

        public List<UserModel> ProjectUsers
        {
            get => _projectUsers;
            set
            {
                _projectUsers = value;
                RaisePropertyChanged(nameof(ProjectUsers));
            }
        }

        private ClientAction _typeActionWithProject;

        public ClientAction TypeActionWithProject
        {
            get => _typeActionWithProject;
            set
            {
                _typeActionWithProject = value;
                RaisePropertyChanged(nameof(TypeActionWithProject));
            }
        }

        

        #endregion



        #region METHODS
        private void OpenNewProject()
        {
            _typeActionWithProject = ClientAction.Create;
            var wnd = new CreateOrUpdateProjectWindow();
            wnd.DataContext = this;
            wnd.ShowDialog();
        }

        private void OpenUpdateProject(object projectId)
        {
            SelectedProject = GetProjectClientById(projectId);

            _typeActionWithProject = ClientAction.Update;
            var wnd = new CreateOrUpdateProjectWindow();
            wnd.DataContext = this;
            wnd.ShowDialog();
        }

        private void ShowProjectInfo(object projectId)
        {
            
            SelectedProject = GetProjectClientById(projectId);
        }


        private ModelClient<ProjectModel> GetProjectClientById(object projectId)
        {
            try
            {
                int id = (int)projectId;
                ProjectModel project = _projectsRequestService.GetProjectsById(_token, id);
                return new ModelClient<ProjectModel>(project);
            }
            catch (FormatException ex)
            {
                return new ModelClient<ProjectModel>(null);
            }
        }

        private void CreateOrUpdateProject()
        {
            if (_typeActionWithProject == ClientAction.Create)
            {
                CreateProject();
            }
            if (_typeActionWithProject == ClientAction.Update)
            {
                UpdateProject();
            }
        }

        private void CreateProject()
        {
           var resultAction =  _projectsRequestService.CreateProject(_token, SelectedProject.Model);
            if (resultAction == System.Net.HttpStatusCode.OK)
            {
                _viewService.ShowMessage(resultAction.ToString() + "\nNew project is created");
            }
            else
            {
                _viewService.ShowMessage(resultAction.ToString() + "\nNew project is not created");
            }
        }

        private void UpdateProject()
        {
            var resultAction = _projectsRequestService.UpdateProject(_token, SelectedProject.Model);
            if (resultAction == System.Net.HttpStatusCode.OK)
            {
                _viewService.ShowMessage(resultAction.ToString() + "\nNew project is updated");
            }
            else
            {
                _viewService.ShowMessage(resultAction.ToString() + "\nNew project is not updated");
            }
        }

        private void DeleteProject()
        {
            var resultAction = _projectsRequestService.DeleteProject(_token, SelectedProject.Model.Id);
            if (resultAction == System.Net.HttpStatusCode.OK)
            {
                _viewService.ShowMessage(resultAction.ToString() + "\nNew project is deleted");
            }
            else
            {
                _viewService.ShowMessage(resultAction.ToString() + "\nNew project is not deleted");
            }
        }

        


        #endregion
    }
}
