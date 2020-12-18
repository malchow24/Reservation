// Reservation.cs
//
// Owen Malchow
// 
// Reservation class.

using System;

[Serializable]
public class Reservation
{
    // public enumeration with constants that represent room type
    public enum RoomTypeEnum { STANDARD, DELUXE, LUXURY };

    // private string array that stores room types
    private static string[] roomTypesValue = { "Standard", "Deluxe", "Luxury" };

    // private instance variable for storing room type
    private int roomTypeValue;

    // private instance variable for storing first name
    private string firstNameValue;

    // private instance variable for storing last name
    private string lastNameValue;

    // private instance variable for storing check out
    private DateTime checkOutValue;

    // parameter-less constructor
    public Reservation()
    {
    }

   // constructor
    public Reservation(string first, string last, DateTime checkIn, DateTime checkOut, bool isMale, int roomType, Address homeAddress, PhoneNumber homePhone, PhoneNumber mobilePhone)
   {
       this.FirstName = first;
       this.LastName = last;
       this.CheckIn = checkIn;
       this.CheckOut = checkOut;
       this.IsMale = isMale;
       this.RoomType = roomType;
       this.HomeAddress = homeAddress;
       this.HomePhone = homePhone;
       this.MobilePhone = mobilePhone;
   } // end Reservation constructor

    // property to get and set reservation's first name
    public string FirstName
    {
        get
        {
            return Util.Capitalize(firstNameValue);
        } // end get
        set
        {
            value = value.Trim().ToUpper();
            if (value.Length < 1)
                throw new ApplicationException("First name is empty!");
            // check for letters
            foreach (char c in value)
                if (c < 'A' || c > 'Z')
                    throw new ApplicationException("First name must consist of letters only!");
            firstNameValue = value;
        } // end set
    } // end property FirstName

    // property to get and set reservation's last name
    public string LastName
    {
        get
        {
            return Util.Capitalize(lastNameValue);
        } // end get
        set
        {
            value = value.Trim().ToUpper();
            if (value.Length < 1)
                throw new ApplicationException("Last name is empty!");
            // check for letters
            foreach (char c in value)
                if (c < 'A' || c > 'Z')
                    throw new ApplicationException("Last name must consist of letters only!");
            lastNameValue = value;
        } // end set
    } // end property LastName

    // read-only property to get name
    public string Name
    {
        get
        {
            return LastName + ", " + FirstName;
        } // end get
    } // end property Name

   // property to get and set reservation's check in
   public DateTime CheckIn { get; set; }

   // property to get and set reservation's check out
   public DateTime CheckOut
   {
       get
       {
           return checkOutValue;
       } // end get
       set
       {
           if (value <= CheckIn)
               throw new ApplicationException("Check out must be after " + CheckIn.ToShortDateString());
           checkOutValue = value;
       } // end set
   }

   // property that gets and sets room type
   public int RoomType
   {
       get
       {
           return roomTypeValue;
       } // end get
       set
       {
           if (value > (int)RoomTypeEnum.LUXURY || value < (int)RoomTypeEnum.STANDARD)
           {
               throw new ApplicationException("Room type ranges from " +
                   ((int)RoomTypeEnum.STANDARD).ToString() + " to " +
                   ((int)RoomTypeEnum.LUXURY).ToString());
           }
           roomTypeValue = value;
       } // end set
   } // end property Rank

   // property that gets and sets room type as a string
   public string RoomTypeName
   {
       get
       {
           return roomTypesValue[roomTypeValue];
       } // end get
       set
       {
           switch (value.Trim().ToUpper())
           {
               case "STANDARD":
               case "S":
                   roomTypeValue = (int)RoomTypeEnum.STANDARD;
                   break;
               case "DELUXE":
               case "D":
                   roomTypeValue = (int)RoomTypeEnum.DELUXE;
                   break;
               case "LUXURY":
               case "L":
                   roomTypeValue = (int)RoomTypeEnum.LUXURY;
                   break;
               default:
                   throw new ApplicationException("Room type must be S, D, or L!");
           }
       } // end set
   } // end property RankName

    // property to get and set whether gender is male
   public bool IsMale { get; set; }

   // property to get and set whether gender is female
   public bool IsFemale
   {
       get
       {
           return !IsMale;
       } // end get
       set
       {
           IsMale = !value;
       } // end set
   } // end property IsFemale

   // property to get and set gender
   public string Gender
   {
       get
       {
           if (IsMale)
               return "Male";
           else
               return "Female";
       } // end get
       set
       {
           value = value.ToUpper();
           if (value == "MALE" || value == "M")
               IsMale = true;
           else if (value == "FEMALE" || value == "F")
               IsMale = false;
           else
               throw new ApplicationException("Gender should be Male or Female!");
       } // end set
   } // end property Gender

   // read-only property to get title
   public string Title
   {
       get
       {
           if (IsMale)
               return "Mr.";
           else
               return "Ms.";
       } // end get
   } // end property Title

   // read-only property to get title and name
   public string TitleName
   {
       get
       {
           return Title + " " + Name;
       } // end get
   } // end property TitleName

   // read-only property to get number of nights
   public int Nights
   {
       get
       {
               return (CheckOut-CheckIn).Days;
       }
   }

   // property to get and set home address
   public Address HomeAddress { get; set; }

   // property to get and set home phone
   public PhoneNumber HomePhone { get; set; }

   // property to get and set mobile phone
   public PhoneNumber MobilePhone { get; set; }

   // virtual readonly property for guest type, will be overridden by derived classes
   public virtual string GuestType
   {
       get
       {
           return "Guest";
       }
   } // no implementation here

   // virtual, read-only property to get the DiscountRate. Will be overriden by derived classes
   virtual public double DiscountRate
   {
       get
       {
           return 0;
       }
   }

   // read-only property to get the Price.
   public decimal Price
   {
       get
       {
           const decimal PriceStandard = 119m;
           const decimal PriceDeluxe = 159m;
           const decimal PriceLuxury = 239m;

           switch (RoomType)
           {
               case (int)RoomTypeEnum.STANDARD: return PriceStandard;
               case (int)RoomTypeEnum.DELUXE: return PriceDeluxe;
               default: return PriceLuxury;
           }
       }
   }

   // read-only property to get the RoomCharge.
   public decimal RoomCharge
   {
       get
       {
           return Price * Nights * (decimal)(1 - DiscountRate / 100);
       }
   }

   // read-only property to get the Tax.
   public decimal Tax
   {
       get
       {
           const decimal TaxRate = 0.05m;
           return RoomCharge * TaxRate;
       }
   }

   // read-only property to get the Total.
   public decimal Total
   {
       get
       {
           return RoomCharge + Tax;
       }
   }

   // returns string representation of reservation object
   public override string ToString()
   {
       return GuestType + " "
       + TitleName + "\t"
       + HomeAddress + "\t"
       + HomePhone.ToString() + ", "
       + MobilePhone.ToString() + ", "
       + Nights + " nights, "
       + RoomTypeName + "\t"
       + "Discount: " + DiscountRate.ToString() + "%  "
       + "Room Charge: " + RoomCharge.ToString("C") + "  "
       + "Tax: " + Tax.ToString("C") + "  "
       + "Total: " + Total.ToString("C");
   } // end method ToString

} // end class Reservation
