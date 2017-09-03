using System.Collections.Generic;
using EL.Persist;

namespace EL.Transfer
{

    public interface ICompanyService
    {
        Company GetCompanyById(int companyId);
        void CreateCompany(Company company);

        List<Address> GetCompanyAddress(int companyId);
        List<Contact> GetCompanyContacts(int companyId);

    }

}