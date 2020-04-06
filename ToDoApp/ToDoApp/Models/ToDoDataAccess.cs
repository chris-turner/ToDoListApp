using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ToDoApp.Models
{
    public class ToDoDataAccess
    {
        HttpClient client = new HttpClient();

        public ToDoList getToDoTasks(Guid userID)
        {
            string jsonString = "";
            userID = new Guid("b6fd0159-d33e-4322-a3b5-34db423d6620");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://localhost:5001/api/todotask/gettasks/{userID}");
            request.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                jsonString = reader.ReadToEnd();
            }
            var jsonObj = JsonConvert.DeserializeObject<dynamic>(jsonString);

            ToDoList tdl = new ToDoList();
            tdl.toDoTasks = new List<ToDoTask>();
            for (int i = 0; i < jsonObj.Count; i++)
            {
                ToDoTask tdt = new ToDoTask();
                tdt.TaskID = jsonObj[i].taskID;
                tdt.UserID = jsonObj[i].userID;
                tdt.TaskDescription = jsonObj[i].taskDescription;
                tdt.TaskStatus = jsonObj[i].taskStatus;
                tdt.TaskImageName = jsonObj[i].taskImageName;
                tdl.toDoTasks.Add(tdt);
            }

            return tdl;

        }

        public ToDoList getToDoTasksComplete(Guid userID)
        {
            string jsonString = "";
            userID = new Guid("b6fd0159-d33e-4322-a3b5-34db423d6620");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://localhost:5001/api/todotask/gettaskscomplete/{userID}");
            request.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                jsonString = reader.ReadToEnd();
            }
            var jsonObj = JsonConvert.DeserializeObject<dynamic>(jsonString);

            ToDoList tdl = new ToDoList();
            tdl.toDoTasks = new List<ToDoTask>();
            for (int i = 0; i < jsonObj.Count; i++)
            {
                ToDoTask tdt = new ToDoTask();
                tdt.TaskID = jsonObj[i].taskID;
                tdt.UserID = jsonObj[i].userID;
                tdt.TaskDescription = jsonObj[i].taskDescription;
                tdt.TaskStatus = jsonObj[i].taskStatus;
                tdt.TaskImageName = jsonObj[i].taskImageName;
                tdl.toDoTasks.Add(tdt);
            }

            return tdl;

        }

        public string AddToDoTask(string taskDesc, string imageName)
        {
            Guid userID = new Guid("b6fd0159-d33e-4322-a3b5-34db423d6620");
            Guid taskID = Guid.NewGuid();
            string taskDescription = taskDesc;
            string taskImageName = imageName;

            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create($"https://localhost:5001/api/todotask");
            request.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "POST";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string jsonStr = "{\"taskID\":\""+ taskID + "\"," +
                             "\"userID\":\"" + userID + "\"," +
                             "\"taskDescription\":\"" + taskDescription + "\"," +
                            "\"taskStatus\":\"PENDING\"," +
                             "\"taskImageName\":\"" + taskImageName + "\"}";
                streamWriter.Write(jsonStr);
            }

           
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public string updateTaskStatusPending(Guid taskID)
        {
            HttpWebRequest request = 
                (HttpWebRequest)WebRequest.Create($"https://localhost:5001/api/ToDoTask/{taskID}/0");
            request.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            request.Method = "PUT";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public string updateTaskStatusComplete(Guid taskID)
        {
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create($"https://localhost:5001/api/ToDoTask/{taskID}/1");
            request.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            request.Method = "PUT";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
        public string DeleteToDoTask(Guid taskID)
        {
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create($"https://localhost:5001/api/todotask/{taskID}");
            request.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            request.Method = "DELETE";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
