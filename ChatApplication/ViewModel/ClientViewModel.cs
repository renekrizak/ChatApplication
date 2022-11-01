using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using ChatClient.Commands;
using ChatClient.Model;
using ChatClient.Store;
using ChatClient.View;
using ChatClient.Net;
using System.Net.Sockets;

namespace ChatClient.ViewModel
{
    public class ClientViewModel : ViewModelBase
    {

        private string _username;
        private string _uid;
        private string _message;
        
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                ObjPropertyChanged();
            }
        }
        
        public ICommand SendMessageCommand { get; }

        public ClientViewModel()
        {

        }
       
    }
}
