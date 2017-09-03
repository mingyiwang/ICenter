using EL.Persist;

namespace EL.Transfer
{

    public interface IContactService
    {

        void CreateContact(Contact contact);
        Contact GetContactById(int contactId);
    }

}