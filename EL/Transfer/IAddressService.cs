using EL.Persist;

namespace EL.Transfer
{

    public interface IAddressService
    {

        void CreateAddress(Address address);
        Address GetAddressById(int addressId);


    }

}