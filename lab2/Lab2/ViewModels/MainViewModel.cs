using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using System.Net.Http;
using System.Text.Json;

using Microsoft.Maui.Controls;
using System.Diagnostics;


namespace Lab2.ViewModels
{
    public class MainViewModel:INotifyPropertyChanged
    {
        private int _taskID = 1;
        private int _page = 1; 
        private string _currentDateTime;

        private string _currentTitle;
        private string _currentTaskID;
        private List<TodoItem> _items;
        public string Title
        {
            get => "Hello World";
        }
        public List<TodoItem> Items {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }
        public int TaskID {
            get => _taskID;
            set
            {
                _taskID = value;
                OnPropertyChanged();
            }
        }
        public int Page
        {
            get => _page;
            set
            {
                _page = value;
                OnPropertyChanged();
            }
        }
        public string CurrentTitle
        {
            get => _currentTitle;
            set
            {
                _currentTitle = "Title:" +value;
                OnPropertyChanged();
            }
        }
        public string CurrentTaskID
        {
            get => _currentTaskID;
            set
            {
                _currentTaskID = "Task ID:" + value;
                OnPropertyChanged();
            }
        }
        public string CurrentDateTime
        {
            get => _currentDateTime;
            set
            {
                _currentDateTime = value;
                OnPropertyChanged();
            }
        }
        
        public ICommand UpdateTimeCommand { get; }
        public ICommand UpdateTaskCommand { get; }
        public ICommand UpdatePageCommand { get; }

        public MainViewModel()
        {
            FetchTodoFromApiAsync(TaskID);
            GetTodosFromApiAsync(Page);
            UpdateTimeCommand = new Command(UpdateTime);
            UpdateTaskCommand = new Command(UpdateTask);
            UpdatePageCommand = new Command(UpdatePage);
            CurrentDateTime = DateTime.Now.ToString("F");

        }
        private void UpdateTime()
        {
            CurrentDateTime = DateTime.Now.ToString("F");
        }
        private void UpdateTask()
        {
            TaskID++;
            FetchTodoFromApiAsync(TaskID);
        }
         private void UpdatePage()
        {
            Page++;
            GetTodosFromApiAsync(Page);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string UserDevice
        {   
            
            get
            {   
                var deviceInfo = new StringBuilder()
                    .AppendLine($"Model: {DeviceInfo.Model}")
                    .AppendLine($"Manufacturer: {DeviceInfo.Manufacturer}")
                    .AppendLine($"Platform: {DeviceInfo.Platform}")
                    .AppendLine($"OS Version: {DeviceInfo.VersionString}");

                return deviceInfo.ToString();
            }
        }
       
        public class TodoItem
        {
            public int userId { get; set; }
            public int id { get; set; }
            public string title { get; set; }
            public bool completed { get; set; }
        }

        private async Task FetchTodoFromApiAsync(int id)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos/"+id);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var todoItem = JsonSerializer.Deserialize<TodoItem>(json);
                CurrentTitle = todoItem.title;
                CurrentTaskID = Convert.ToString(todoItem.id);

            }
            
        }
        private async Task GetTodosFromApiAsync(int page)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts?_page="+page+"&_limit=5");

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var todoItem = JsonSerializer.Deserialize<List<TodoItem>>(json);
                Items = todoItem;

            }

        }




    }
}
