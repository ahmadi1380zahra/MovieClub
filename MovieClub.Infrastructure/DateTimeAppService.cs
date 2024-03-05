using MovieClub.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MovieClub.Infrastructure
{
    public class DateTimeAppService : DateTimeService
    {
        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
