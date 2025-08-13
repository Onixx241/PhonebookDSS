using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public class Contact
{
    public int ID { get; set; }

    public string User { get; set; }

    public string firstName { get; set; }

    public string lastName { get; set; }

    public string address { get; set; }

    public string city { get; set; }

    public string State { get; set; }
    public int zip { get; set; }

    //public List<Phone> Numbers { get; set; }

}