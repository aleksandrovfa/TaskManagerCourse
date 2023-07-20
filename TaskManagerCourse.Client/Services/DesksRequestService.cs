using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using TaskManagerCourse.Client.Models;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Client.Services
{
    public class DesksRequestService : CommonRequestService
    {
        private string _desksControllerUrl = HOST + "desks";

        public List<DeskModel> GetAllDesks(AuthToken token)
        {
            string response = GetDataByUrl(HttpMethod.Get, _desksControllerUrl, token);
            List<DeskModel> desks = JsonConvert.DeserializeObject<List<DeskModel>>(response);
            return desks;
        }

        public DeskModel GetDesksById(AuthToken token, int deskId)
        {
            var response = GetDataByUrl(HttpMethod.Get, _desksControllerUrl + $"/{deskId}", token);
            DeskModel desk = JsonConvert.DeserializeObject<DeskModel>(response);
            return desk;
        }
        public List<DeskModel> GetDeskByProject(AuthToken token, int projectId)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("projectId",projectId.ToString());
            string response = GetDataByUrl(HttpMethod.Get, _desksControllerUrl+ "/project", token, null,null,parameters);
            List<DeskModel> desks = JsonConvert.DeserializeObject<List<DeskModel>>(response);
            return desks;
        }

        public HttpStatusCode CreateDesk(AuthToken token, DeskModel desk)
        {
            string deskJson = JsonConvert.SerializeObject(desk);
            var result = SendDataByUrl(HttpMethod.Post, _desksControllerUrl, token, deskJson);
            return result;
        }

        public HttpStatusCode UpdateDesk(AuthToken token, DeskModel desk)
        {
            string deskJson = JsonConvert.SerializeObject(desk);
            var result = SendDataByUrl(HttpMethod.Patch, _desksControllerUrl + $"/{desk.Id}", token, deskJson);
            return result;
        }


        public HttpStatusCode DeleteDesk(AuthToken token, int deskId)
        {
            var result = DeleteDataByUrl(_desksControllerUrl + $"/{deskId}", token);
            return result;
        }
    }
}
