using Moq;
using MovieClub.Contracts.Interfaces;
using MovieClub.Persistence.EF;
using MovieClub.Persistence.EF.Films;
using MovieClub.Persistence.EF.Rents;
using MovieClub.Persistence.EF.Users;
using MovieClub.Services.Rents.RentManangers;
using MovieClub.Services.Rents.RentManangers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClub.Tests.Tools.Rents
{
    public static class RentManangerServiceFactory
    {
        public static RentManangerService Create(EFDataContext context,DateTime? _fakeDate=null)
        {
            var dateTimeServiceMock = new Mock<DateTimeService>();
            dateTimeServiceMock.Setup(_ => _.UtcNow()).Returns(_fakeDate??new DateTime(2024,1,2));
            return new RentManangerAppService(new EFRentManangerRepository(context)
                                              ,new EFUnitOfWork(context) 
                                              ,new EFFilmRepository(context)
                                              ,dateTimeServiceMock.Object
                                              ,new EFUserMananengerRepository(context));
        }
             
    }
}
