﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaskManagerCourse.Client.Models;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Client.Services
{
    public class ProjectRequestService : CommonRequestService
    {
        private string _projectsControllerUrl = HOST + "projects";
        public List<ProjectModel> GetAllProjects(AuthToken token)
        {
            string response = GetDataByUrl(HttpMethod.Get, _projectsControllerUrl, token);
            List<ProjectModel> projects = JsonConvert.DeserializeObject<List<ProjectModel>>(response);
            return projects;
        }
        public ProjectModel GetProjectsById(AuthToken token, int projectId)
        {
            var response= GetDataByUrl(HttpMethod.Get, _projectsControllerUrl + $"/{projectId}", token);
            ProjectModel project = JsonConvert.DeserializeObject<ProjectModel>(response);
            return project;
        }

        public HttpStatusCode CreateProject(AuthToken token, ProjectModel project)
        {
            string projectJson = JsonConvert.SerializeObject(project);
            var result = SendDataByUrl(HttpMethod.Get, _projectsControllerUrl, token, projectJson);
            return result;
        }

        public HttpStatusCode UpdateProject(AuthToken token, ProjectModel project)
        {
            string projectJson = JsonConvert.SerializeObject(project);
            var result = SendDataByUrl(HttpMethod.Patch, _projectsControllerUrl + $"/{project.Id}", token, projectJson);
            return result;
        }

        public HttpStatusCode DeleteProject(AuthToken token, int projectId)
        {
            var result = DeleteDataByUrl(_projectsControllerUrl + $"/{projectId}", token);
            return result;
        }

        public HttpStatusCode AddUsersToProject(AuthToken token,int projectId, List<int> userIds)
        {
            string useridsJson = JsonConvert.SerializeObject(userIds);
            var result = SendDataByUrl(HttpMethod.Patch, _projectsControllerUrl + $"/{projectId}/users", token, useridsJson);
            return result;
        }

        public HttpStatusCode RemoveUsersFromProject(AuthToken token, int projectId, List<int> userIds)
        {
            string useridsJson = JsonConvert.SerializeObject(userIds);
            var result = SendDataByUrl(HttpMethod.Patch, _projectsControllerUrl + $"/{projectId}/remove", token, useridsJson);
            return result;
        }
    }
}
