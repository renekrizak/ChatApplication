﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChatClient.Net;
using ChatClient.Store;
using ChatClient.ViewModel;

namespace ChatClient.Commands
{
    public class RegisterUserCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;

        public RegisterUserCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            var values = (object[])parameter;
            var regData = $"{(string)values[0]}|{(string)values[1]}|{(string)values[2]}";
            var conn = new Server();
            conn.RegisterConnectToServer(regData);
            _navigationStore.CurrentViewModel = new ClientViewModel();

        }
    }
}
