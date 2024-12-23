﻿using ChatClient.Store;
using ChatClient.ViewModel;

namespace ChatClient.Commands
{
    public class NavigateLoginViewCommand : CommandBase
    {

        private readonly NavigationStore _navigationStore;

        public NavigateLoginViewCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new LoginViewModel(_navigationStore);
        }
    }
}
