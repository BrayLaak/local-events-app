using local_events_app.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace local_events_app.UI
{
    public class ConsoleUI
    {
        private readonly MeetupController _meetupController;

        public ConsoleUI(MeetupController meetupController)
        {
            _meetupController = meetupController;
        }

        // Methods for displaying and interacting with the console UI
        public void Run()
        {
            // Console UI loop implementation tbd
            // ...
        }
    }
}
