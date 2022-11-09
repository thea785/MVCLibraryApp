using System.Collections.Generic;
using System.Net;

namespace LibraryWebApplication.Models
{
    public class DashboardModel
    {
        public List<BookModel> books { get; set; }
        public List<DisplayUserModel> users { get; set; }
    }
}
