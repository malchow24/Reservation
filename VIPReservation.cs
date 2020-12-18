using System;

[Serializable]
public class VIPReservation : Reservation
{
    // List of the class data members
    private DateTime startingDateValue;

            // parameter-less constructor
    public VIPReservation()
        : base()
    {
    }

    // constructor
    public VIPReservation(string first, string last, DateTime checkIn, DateTime checkOut, bool isMale, char roomType, Address homeAddress, PhoneNumber homePhone, PhoneNumber mobilePhone,
        DateTime startingDate)
        : base(first, last, checkIn, checkOut, isMale, roomType, homeAddress, homePhone, mobilePhone)
   {
       this.StartingDate = startingDate; 
   } // end Lecturer constructor
        
    // Read or write the mStartDate data member
    public DateTime StartingDate
    {
        get
        {
            return startingDateValue;
        }
        set
        {
            if (value > DateTime.Now)
            {
                ApplicationException exVar = new ApplicationException("Start date must be prior to today!");
                throw exVar;
            }
            startingDateValue = value;
        }
    }

    // override guest type property
    public override string GuestType
    {
        get
        {
            return "VIP";
        }
    }

    // Returns the DiscountRate.
    override public double DiscountRate
    {
        get
        {
            const double DiscountRateVIP = 10;
            return DiscountRateVIP;
        }
    }

    // returns string representation of VIP reservation object
    public override string ToString()
    {
        return base.ToString() + "  Since " + StartingDate.ToShortDateString();
    } // end method ToString
}
