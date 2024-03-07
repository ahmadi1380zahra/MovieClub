using MovieClub.Persistence.EF;

namespace MovieClub.Tests.Tools.Infrastructure.DatabaseConfig.Unit;

public class BusinessUnitTest
{
    protected EFDataContext DbContext { get; }
    protected EFDataContext SetupContext { get; }
    protected EFDataContext ReadContext { get; }


    protected BusinessUnitTest()
    {
        var db = new EFInMemoryDatabase();
        
        DbContext = db.CreateDataContext<EFDataContext>();
        SetupContext = db.CreateDataContext<EFDataContext>();
        ReadContext = db.CreateDataContext<EFDataContext>();
    }
}