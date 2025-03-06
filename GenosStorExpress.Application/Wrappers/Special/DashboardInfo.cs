namespace GenosStorExpress.Application.Wrappers.Special {
    public class DashboardInfo {
        public string LoggedAdmin { get; set; }
        public int RegisteredUsers { get; set; }
        public int RegisteredIndividualEntities { get; set; }
        public int RegisteredLegalVerifiedEntities { get; set; }
        public int LegalEntitiesWaitingForVerification { get; set; }
        public int ActiveOrdersCount { get; set; }
        public LastOrderDashboardInfo LastOrder { get; set; }
    }

    public class LastOrderDashboardInfo {
        public long Id { get; set; }
        public string Orderer { get; set; }
        public string CreatedAt { get; set; }
        public double Total { get; set; }
        public int ItemsCount { get; set; }
    }
}