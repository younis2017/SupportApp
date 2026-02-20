using SupportApp.Domain.Entities;


namespace SupportApp.Application.Services
    {
    public interface ICustomerTenetService
    {
        string GenerateCustomerTenet(Customer customer);
    }

    //public class CustomerTenetService : ICustomerTenetService
    //{
    //    public string GenerateCustomerTenet(Customer customer)
    //    {
    //        var name = customer.CustomerName ?? "CUST";

    //        var letters = new string(
    //            name.Where(char.IsLetter).ToArray()
    //        );

    //        var last4 = letters.Length >= 4
    //            ? letters[^4..]
    //            : letters.PadLeft(4, 'X');

    //        var last3Digits = customer.CustomerId
    //            .ToString()
    //            .PadLeft(3, '0');

    //        return (last4 + last3Digits).ToUpper();
    //    }
    //}
}
