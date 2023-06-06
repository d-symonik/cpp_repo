using Lab2.ViewModels;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;

namespace Lab2.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void IsStartedFromFirstPage()
        {
            var viewModel = new MainViewModel();
            Assert.Equal(1, viewModel.Page);
        }
        [Fact]
        public void IsStartedFromFirstToDo()
        {
            var viewModel = new MainViewModel();
            Assert.Equal(1, viewModel.TaskID);
        }
        [Fact]
        public void IsTitleExist()
        {
            var viewModel = new MainViewModel();
            Assert.Equal("Hello World", viewModel.Title);
        }
    }
}
