using Applications.Service.Common.Entities;

namespace Libraries.Tests.Common.Constants;

public class InitialDatabaseValuesConstant
{
    public static readonly CustomerEntity[] Customers = new[]
    {
        new CustomerEntity
        {
            CustomerId = "withAccount0", Name = "John", Surname = "Doe",
            CurrentAccount = new CurrentAccountEntity() { Balance = 0 }
        },
        new CustomerEntity
        {
            CustomerId = "withoutAccount1", Name = "Jane", Surname = "Smith"
        },
        new CustomerEntity
        {
            CustomerId = "withoutAccount2", Name = "Jane", Surname = "Smith"
        },
        new CustomerEntity
        {
            CustomerId = "withAccount1000", Name = "Chris", Surname = "Miller",
            CurrentAccount = new CurrentAccountEntity() { Balance = 1000 }
        },
    };
}