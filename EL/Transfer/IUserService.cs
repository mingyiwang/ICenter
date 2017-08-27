using System.Collections.Generic;
using EL.Persist;

namespace EL.Transfer
{

    public interface IUserService
    {
        /**
         * User GRUD Operations
         */
        User GetUserById(int userId);
        User GetUserByName(string userName);
        User GetUserByEmail(string  email);
        User GetUserByMobile(string mobile);

        void CreateUser(User user);
        void DeleteUser();
        void UpdateUser(User user);
        List<User> SearchUsers();
        List<User> ListUsers();

        /**
           Person GRUD Operations 
        */
        Person GetPerson();
        void AddPerson(Person person);
        void DeletePerson();
        void UpdatePerson(Person person);
        List<Person> SearchPersons();
        List<Person> ListPersons();
        
        Person GetPersonByUserId(int userId);
        User   GetUserByPersonId(int personId);

    }

}