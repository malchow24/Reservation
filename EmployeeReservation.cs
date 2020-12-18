using System;

[Serializable]
public class EmployeeReservation : Reservation
{
    // List of the class data members
    private int employeeIDValue;

        // parameter-less constructor
    public EmployeeReservation()
        : base()
    {
    }

    // constructor
    public EmployeeReservation(string first, string last, DateTime checkIn, DateTime checkOut, bool isMale, char roomType, Address homeAddress, PhoneNumber homePhone, PhoneNumber mobilePhone,
        int employeeID)
        : base(first, last, checkIn, checkOut, isMale, roomType, homeAddress, homePhone, mobilePhone)
   {
       this.EmployeeID = employeeID; 
   } // end Lecturer constructor


    // Read or write the employeeIDValue data member
    public int EmployeeID
    {
        get
        {
            return employeeIDValue;
        }
        set
        {
            if (value < 1000 || value > 9999)
            {
                ApplicationException exVar = new ApplicationException("Reservation ID must be a number between 1000 and 9999!");
                throw exVar;
            }
            employeeIDValue = value;
        }
    }

    // override guest type property
    public override string GuestType
    {
        get
        {
                return "Reservation";
        }
    }

    // Returns the DiscountRate.
    override public double DiscountRate
    {
        get
        {
            const double DiscountRateReservation = 15;
            return DiscountRateReservation;
        }
    }

    // returns string representation of Reservation reservation object
    public override string ToString()
    {
        return base.ToString() + "  Reservation ID: " + EmployeeID.ToString();
    } // end method ToString
}
