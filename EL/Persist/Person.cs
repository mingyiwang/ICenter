﻿using System;

namespace EL.Persist
{

    public class Person
    {
        public int      Id         { get; set; }
        public string   FirstName  { get; set; }
        public string   MiddleName { get; set; }
        public string   LastName   { get; set; }
        public int      Age        { get; set; }
        public int      Sex        { get; set; }
        public DateTime DateBirth  { get; set; }

    }

}