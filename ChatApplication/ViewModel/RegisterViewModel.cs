using ChatClient.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.ViewModel
{
    class RegisterViewModel
    {
        public RelayCommand RegisterUser { get; set; }
        public RelayCommand ChangeViewToLogin { get; set; }
        public RelayCommand ChangeViewToHome { get; set; }

        public RegisterViewModel()
        {

        }



    }
}
