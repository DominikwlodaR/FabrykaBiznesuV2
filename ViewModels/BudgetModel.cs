using FabrykaBiznesuV2.Models;

namespace FabrykaBiznesuV2.ViewModels
{
    public class BudgetModel
    {
        public Project? Project { get; set; }
        public AppUser? appUser { get; set; }
        public Budget? BAdd { get; set; }
        public List<Budget>? Budgets { get; set; }
        public double MoneySpend { get; set; }
        public double NoAccept {  get; set; }
        public int AcceptedCounter { get; set; }
        public int RejectedCounter {  get; set; }
        public int WaintingCounter {  get; set; }




    }
}
