namespace GenosStorExpress.Domain.Entity.User;

public sealed class VoidCustomer: Customer {
    public override UserType UserType => UserType.Administrator;
}