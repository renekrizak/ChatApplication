using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatClient.ViewModel;
using ChatClient.Store;
using ChatClient.Net;
using System.Diagnostics;
using System.Threading;

namespace ChatClient.Commands
{
    public class NavigateClientViewCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;

      
       
        public NavigateClientViewCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            
 
        }
        public override void Execute(object parameter)
        {
            
            /*Task.Run(() =>
            {
                while (true)
                {

                    Debug.WriteLine($"Name: {Username}");
                    Debug.WriteLine($"Password: {Password}");
                    Thread.Sleep(500);
                }
            }

            );*/

            var conn = new Server();
            
            //conn.ConnectToServer();
           // conn.LoginConnectToServer();

            _navigationStore.CurrentViewModel = new ClientViewModel();
        }

    }
}
