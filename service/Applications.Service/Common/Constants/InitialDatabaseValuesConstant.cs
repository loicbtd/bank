using Applications.Service.Common.Entities;

namespace Applications.Service.Common.Constants;

public class InitialDatabaseValuesConstant
{
    public static readonly CustomerEntity[] Customers = new[]
    {
        new CustomerEntity
        {
            CustomerId = "jdoe", Name = "John", Surname = "Doe",
            CurrentAccount = new CurrentAccountEntity() { Balance = 0 }
        },
        new CustomerEntity
        {
            CustomerId = "jsmith", Name = "Jane", Surname = "Smith",
            CurrentAccount = new CurrentAccountEntity() { Balance = 0 }
        },
        new CustomerEntity { CustomerId = "ejohnson", Name = "Emily", Surname = "Johnson" },
        new CustomerEntity { CustomerId = "mwilliams", Name = "Michael", Surname = "Williams" },
        new CustomerEntity { CustomerId = "dbrown", Name = "David", Surname = "Brown" },
        new CustomerEntity
        {
            CustomerId = "sjones", Name = "Sarah", Surname = "Jones",
            CurrentAccount = new CurrentAccountEntity() { Balance = 1000 }
        },
        new CustomerEntity
        {
            CustomerId = "cmiller", Name = "Chris", Surname = "Miller",
            CurrentAccount = new CurrentAccountEntity() { Balance = 2000 }
        },
        new CustomerEntity { CustomerId = "jdavis", Name = "Jessica", Surname = "Davis" },
        new CustomerEntity { CustomerId = "dgarcia", Name = "Daniel", Surname = "Garcia" },
        new CustomerEntity { CustomerId = "lmartinez", Name = "Lisa", Surname = "Martinez" }
    };
}