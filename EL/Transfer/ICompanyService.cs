using System.Collections.Generic;
using EL.Persist;

namespace EL.Transfer
{

    public interface ICompanyService
    {

        Company GetCompanyById(int companyId);
        void AddNewCompany(Company company);

        List<Address> GetCompanyAddress(int companyId);
        List<Contact> GetCompanyContacts(int companyId);

        List<CompanyStatisticSnapshot> GetCompanyStatistics(int companyId);
        CompanyStatisticSnapshot GetCompanyStatistic(int companyId, int year);
        CompanyStatistic GetCompanyStatisticFull(int statisticId);

    }

}