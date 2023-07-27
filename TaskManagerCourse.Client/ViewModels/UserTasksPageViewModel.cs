using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using TaskManagerCourse.Client.Models;
using TaskManagerCourse.Client.Services;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Client.ViewModels
{
    public class UserTasksPageViewModel : BindableBase
    {
        private AuthToken _token;

        private TasksRequestService _taskRequestSevice;
        private UsersRequestService _usersRequestSevice;
        public UserTasksPageViewModel(AuthToken token)
        {
            _token = token;
            _taskRequestSevice = new TasksRequestService();
            _usersRequestSevice = new UsersRequestService();
        }

        public List<TaskClient> AllTasks
        {
            get => _taskRequestSevice.GetAllTasks(_token)
                .Select(task =>
                {
                    var taskClient = new TaskClient(task);
                    if (task.CreatorId != null)
                    {
                        int creatorId = (int)task.CreatorId;
                        taskClient.Creator = _usersRequestSevice.GetUserById(_token, creatorId);
                    }
                    if (task.ExecutorId != null)
                    {
                        int executorId = (int)task.ExecutorId;
                        taskClient.Executor = _usersRequestSevice.GetUserById(_token, executorId);
                    }
                    return taskClient;
                }
                )
                .ToList();
        }
    }
}
