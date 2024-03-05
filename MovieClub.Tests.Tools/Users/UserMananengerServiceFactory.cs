using DoctorAppointment.Persistance.EF;
using Microsoft.EntityFrameworkCore;
using Moq;
using MovieClub.Contracts.Interfaces;
using MovieClub.Persistence.EF;
using MovieClub.Persistence.EF.Users;
using MovieClub.Services.Users.UserMananger;
using MovieClub.Services.Users.UserMananger.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Tests.Tools.Users
{
    public static class UserMananengerServiceFactory
    {
        public static UserMananengerService Create(EFDataContext context,DateTime? fakeTime = null)
        {
            var mockDateTimeService = new Mock<DateTimeService>();
            mockDateTimeService.Setup(_ => _.UtcNow()).Returns(fakeTime ?? new DateTime(2024, 2, 1));
            return new UserMananengerAppService(new EFUserMananengerRepository(context),
                                                 new EFUnitOfWork(context)
                                                 ,mockDateTimeService.Object);
        }
    }
}
