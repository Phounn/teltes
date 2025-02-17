using TelbizTemplate_Test.Components.Models;

namespace TelbizTemplate_Test.Components.Pages
{
    public partial class TestLogic
    {
        public List<CompanyModel> tests { get; set; } = new()
        {
            new CompanyModel{Name="1", CatchPhrase="Bill" },
            new CompanyModel{Name="2", CatchPhrase="Nita" },
            new CompanyModel{Name="3", CatchPhrase="Yok"},
        };
    }
}
