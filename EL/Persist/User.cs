using System;

namespace EL.Persist
{
    public class User
    {
        public int Id                  { get; set; }
        public int PersonId            { get; set; }
        public string UserName         { get; set; }
        public string Password         { get; set; }
        public DateTime DateRegistered { get; set; }
        public DateTime DateLastLogIn  { get; set; }
        public DateTime DateUpdated    { get; set; }

    }

}